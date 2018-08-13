using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GradeProject.ProfileService.Communication;
using GradeProject.ProfileService.Communication.CommandHandlers;
using GradeProject.ProfileService.Config;
using GradeProject.ProfileService.Infrastructure;
using GradeProject.ProfileService.Infrastructure.Repos;
using GradeProject.ProfileService.Infrastructure.Services;
using GradeProject.ProfileService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GradeProject.ProfileService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IEventBus RabbitMqBus { get; set; }
        public IContainer AppContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        // SET THIS TO true IN PRODUCTION!

                        options.ApiSecret = "secret";
                        options.Authority = "http://localhost:5000";
                        options.ApiName = "Platform.ProfileService";
                    });

            //Added Automapper
            services.AddAutoMapper();

            //Cors
            services.AddCors(opts =>
            {
                opts.AddPolicy("AllowAll", conf => conf.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                //opts.AddPolicy("AuthServiceOnly", conf => conf.WithOrigins("https://localhost:44362").WithMethods("POST"));
            });

            AppContainer = RegisterDependencies(services);

            RabbitMqBus = AppContainer.Resolve<IEventBus>();

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

            app.UseCors("AllowAll");
            app.UseStaticFiles();
            app.UseMvc();
        }

        private IContainer RegisterDependencies(IServiceCollection services)
        {
            //Configuration
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));
            services.Configure<RabbitMqConfig>(Configuration.GetSection("RabbitMqConfig"));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            var assembly = Assembly.GetExecutingAssembly();

            //Context
            builder.RegisterType<MongoDbContext>()
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.Register(c => new MongoDbSettings());
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()));
            //Repos
            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            builder.Register(c => new AddProfileCommandHandler(c.Resolve<IUserService>()));

            builder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
