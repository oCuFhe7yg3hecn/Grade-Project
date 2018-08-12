using System;
using System.Collections.Generic;
using Autofac;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using GradeProject.ScoreService.Controllers;
using GradeProject.ScoreService.Infrastructure;
using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GradeProject.ScoreService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public IContainer AppContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
                //.AddJsonOptions(options =>
                //{
                //    options.SerializerSettings.ReferenceLoopHandling =
                //        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                //}); ;

            services.AddLogging();

            services.AddCors(conf => conf.AddPolicy("AllowAll", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyMethod()));

            services.AddAuthentication("Bearer")
               .AddIdentityServerAuthentication(options =>
               {
                   options.RequireHttpsMetadata = true;

                   options.Authority = "https://localhost:44362";
                   options.ApiName = "Platform.ScoreService";
               });

            //Register Dependencies
            AppContainer = RegisterDependencies(services);

            return new AutofacServiceProvider(AppContainer);
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


            app.UseMvc();
        }

        private Autofac.IContainer RegisterDependencies(IServiceCollection services)
        {
            //Configurations
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Context
            builder.RegisterType<MongoDbContext>()
                   .AsSelf();

            //Repos
            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IRepository<>))
                .InstancePerLifetimeScope();

            var assembly = Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly)
                   .AsImplementedInterfaces();

            return builder.Build();
        }
    }
}
