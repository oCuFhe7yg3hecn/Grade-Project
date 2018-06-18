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
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Cookies";
                options.DefaultChallengeScheme = "oidc";
            })
                .AddCookie("Cookies")
                .AddOpenIdConnect("oidc", options =>
                {
                    options.SignInScheme = "Cookies";

                    options.Authority = "http://localhost:44313";
                    options.RequireHttpsMetadata = false;

                    options.ClientId = "mvc";
                    options.SaveTokens = true;
                });

            services.AddMvc();

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

            var contRootPath = env.ContentRootPath;

            app.UseAuthentication();

            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
