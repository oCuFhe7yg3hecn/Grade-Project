using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
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

        public Autofac.IContainer AppContainer { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc()
                .AddJsonOptions(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling =
                        Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                }); ;


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

            app.UseMvc();
        }

        private Autofac.IContainer RegisterDependencies(IServiceCollection services)
        {
            //Configurations
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            var sp = services.BuildServiceProvider();

            var mongoSettings = sp.GetService<IOptions<MongoDbSettings>>();

            var builder = new ContainerBuilder();

            //Context
            builder.RegisterInstance(mongoSettings.Value).SingleInstance();
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()))
                                                                    .InstancePerLifetimeScope();

            //Repos
            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IRepository<>))
                                                                    .InstancePerLifetimeScope();

            //Services
            builder.Register(c => new UserService(c.Resolve<IRepository<Domain.User>>(new NamedParameter("collectionName", "Users"))))
                                                                                                                            .As<IUserService>()
                                                                                                                            .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
