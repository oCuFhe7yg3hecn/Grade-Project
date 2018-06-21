using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GradeProject.ProfileService.Infrastructure;
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
            services.AddCors(opts => opts.AddPolicy("AllowAll", conf => conf
                                                                    .AllowAnyOrigin()
                                                                    .AllowAnyMethod()));

            services.AddMvc();

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            //Platform.ProfileService

            services.AddAuthentication("Bearer")
                    .AddIdentityServerAuthentication(options =>
                    {

                        // === FOR DEMO ONLY
                        options.RequireHttpsMetadata = false;
                        // SET THIS TO true IN PRODUCTION!

                        options.Authority = "http://localhost:5000";
                        options.ApiName = "Platform.ProfileService";
                        //options.ApiSecret = "profileService-secret";
                    });

            //services.AddAut

            //services.AddAuthentication(options =>
            //{
            //    options.DefaultScheme = "Cookies";
            //    options.DefaultChallengeScheme = "oidc";
            //})
            //    .AddCookie("Cookies")
            //    .AddOpenIdConnect("oidc", options =>
            //    {
            //        options.SignInScheme = "Cookies";

            //        options.Authority = "http://localhost:5000";
            //        options.RequireHttpsMetadata = false;

            //        options.ClientId = "mvc";
            //        options.SaveTokens = true;
            //    });

            //services.AddMvc();

            services.AddAuthentication();

            services.Configure<MongoDbSettings>(opts =>
            {
                opts.ConnectionString = Configuration["MongoDbSettings:ConnectionString"];
                opts.Database = Configuration["MongoDbSettings:Database"];
            });

            services.AddAutoMapper();

            var builder = new ContainerBuilder();

            builder.Register(c => new MongoDbSettings());
            builder.Populate(services);
            builder.Register(c => new MongoDbContext(c.Resolve<IOptions<MongoDbSettings>>()));

            builder.Register(c => new UserRepository(c.Resolve<MongoDbContext>()));

            builder.Register(c => new UserService(c.Resolve<UserRepository>(), c.Resolve<IMapper>(), c.Resolve<DefaultAvatarsFactory>()));

            //Helper Seervices
            builder.Register(c => new DefaultAvatarsFactory());

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

            //app.UseAuthentication();

            app.UseCors("AllowAll");

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
