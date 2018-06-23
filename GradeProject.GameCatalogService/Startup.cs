using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using GradeProject.GameCatalogService.Controllers;
using GradeProject.GameCatalogService.Filters;
using GradeProject.GameCatalogService.Infrastructure;
using GradeProject.GameCatalogService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

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

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(opts => {
                opts.Filters.Add(typeof(ApiExceptionFilter));
            });

            services.Configure<MongoDbSettings>(opts =>
            {
                opts.ConnectionString = Configuration["MongoDbSettings:ConnectionString"];
                opts.Database = Configuration["MongoDbSettings:Database"];
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Context
            builder.Register(c => new MongoDbSettings());
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()));

            //Repos
            builder.Register(c => new GamesRepository(c.Resolve<MongoDbContext>()));

            //Services
            builder.Register(c => new GamesService(c.Resolve<GamesRepository>()));

            AppContainer = builder.Build();

            return new AutofacServiceProvider(this.AppContainer);

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
    }
}
