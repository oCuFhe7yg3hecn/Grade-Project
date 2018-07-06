using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GradeProject.GameRegService.Communication;
using GradeProject.GameRegService.Config;
using GradeProject.GameRegService.Infrastructure;
using GradeProject.GameRegService.Infrstructure;
using GradeProject.GameRegService.Models;
using GradeProject.ProfileService.Infrastructure.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GradeProject.GameRegService
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddLogging();

            services.AddMvc();

            services.AddAutoMapper();

            //Cors
            services.AddCors(opts => opts.AddPolicy("AllowAll", conf => conf.AllowAnyOrigin()
                                                                            .AllowAnyMethod()
                                                                            .AllowAnyHeader()));

            //Rrgister Dependencies
            AppContainer = RegisterDependencies(services);

            _rabbitMq = AppContainer.Resolve<IEventBus>();

            return new AutofacServiceProvider(this.AppContainer);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseMvc();
        }

        private IContainer RegisterDependencies(IServiceCollection services)
        {
            //Configuration
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.Configure<RabbitMqConfig>(Configuration.GetSection("RabbitMqConfig"));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Utils
            builder.Register(c => new RabbitMqEventBus(c.Resolve<IOptions<RabbitMqConfig>>(),
                                                       c.Resolve<ILogger<RabbitMqEventBus>>()))
                                                                                        .As<IEventBus>();

            //Context
            builder.Register(c => new MongoDbSettings());
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()));

            //Repos
            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //Services
            builder.Register(c => new RegistrationService(c.Resolve<IMapper>(),
                                                          c.Resolve<IEventBus>(),
                                                          c.Resolve<IRepository<GameRegInfo>>(new NamedParameter("collectionName", "RegInfo"))))
                                                                                                                                        .As<IRegistrationService>();


            return builder.Build();
        }
    }
}
