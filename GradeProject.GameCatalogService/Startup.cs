using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GradeProject.GameCatalogService.Communication;
using GradeProject.GameCatalogService.Communication.CommandHandlers;
using GradeProject.GameCatalogService.Communication.Commands;
using GradeProject.GameCatalogService.Configurations;
using GradeProject.GameCatalogService.Filters;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Infrastructure.Repos;
using GradeProject.GameCatalogService.Infrastructure.Services;
using GradeProject.GameCatalogService.Models;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;

namespace GradeProject.GameCatalogService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer AppContainer { get; set; }
        private IEventBus _rabbitMq;
        private ICommandBus _commandBus;

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts =>
            {
                opts.Filters.Add(typeof(ApiExceptionFilter));
            });

            //AutoMapper
            services.AddAutoMapper();

            //OData
            services.AddOData();

            //Add Authentication
            services.AddAuthentication("Bearer")
                  .AddIdentityServerAuthentication(options =>
                  {
                      // SET THIS TO true IN PRODUCTION!
                      options.RequireHttpsMetadata = false;

                      options.Authority = "http://localhost:5000";
                      options.ApiName = $"Platform.GameCatalogService";
                  });

            //REgister Dependencies
            AppContainer = RegisterDependencies(services);

            _commandBus = AppContainer.Resolve<ICommandBus>();
            _commandBus.DependencyResolver = AppContainer;
            
            _rabbitMq = AppContainer.Resolve<IEventBus>(new TypedParameter(typeof(ICommandBus), _commandBus));


            return new AutofacServiceProvider(this.AppContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc(routebuilder =>
            {
                //Enabling OData routing.
                routebuilder.MapODataServiceRoute("ODataRoutes", "odata", ODataConfig.GetBuilder().GetEdmModel());
            });

            app.UseMvc();
        }

        private IContainer RegisterDependencies(IServiceCollection services)
        {
            //Configurations
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.Configure<RabbitMqConfig>(Configuration.GetSection("RabbitMqConfig"));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Utils

            builder.Register(c => new CommandBus())
                                            .As<ICommandBus>()
                                            .SingleInstance();

            builder.Register(c => new RabbitMqBus(c.Resolve<IOptions<RabbitMqConfig>>(),
                                                  c.Resolve<ICommandBus>()))
                                                                       .As<IEventBus>();

            //Context
            builder.Register(c => new MongoDbSettings());
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()));

            //Repos
            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //Services
            builder.Register(c => new GamesService(c.Resolve<IRepository<GameInfo>>(new NamedParameter("collectionName", "GamesData")),
                                                   c.Resolve<IMapper>()))
                                                                      .As<IGamesService>()
                                                                      .InstancePerLifetimeScope();

            builder.Register(c => new CategoryService(c.Resolve<IRepository<Category>>(new NamedParameter("collectionName", "Categories")),
                                                      c.Resolve<IRepository<GameInfo>>(new NamedParameter("collectionName", "GamesData"))))
                                                                                                                                   .As<ICategoryService>()
                                                                                                                                   .InstancePerLifetimeScope();

            //CommandHandlers
            builder.Register(c => new GameRegisteredCommandHandler(c.Resolve<IGamesService>()))
                                                                        .As<ICommandHandler<RegisterGameCommand>>();

            return builder.Build();
        }
    }
}
