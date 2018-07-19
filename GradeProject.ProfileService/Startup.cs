using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
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
            
            AppContainer = RegisterDependencies(services);

            //Cors
            services.AddCors(opts => opts.AddPolicy("AllowAll", conf => conf.AllowAnyOrigin()
                                                                            .AllowAnyMethod()));

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

            //Context
            builder.Register(c => new MongoDbSettings());
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()));

            //Repos
            builder.RegisterGeneric(typeof(GenericRepo<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //Services
            builder.Register(c => new UserService(c.Resolve<IRepository<User>>(new NamedParameter("collectionName", "Users")), 
                                                  c.Resolve<IMapper>()))
                                                                 .As<IUserService>()
                                                                 .InstancePerLifetimeScope();

            return builder.Build();
        }
    }
}
