using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GradeProject.AuthService.MongoInfrastructure;
using IdentityServer4;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GradeProject.AuthService
{
    //For reviewers : Поки що весь інмеморі, бо буду писати майже кастомний. Треба більше часу, а ревю вже близбко
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(opts =>
            {
                opts.Database = Configuration["MongoDbSettings:Database"];
                opts.ConnectionString = Configuration["MongoDbSettings:ConnectionString"];
            });

            services.AddTransient<MongoDbSettings>();
            services.AddTransient<MongoDbContext>();


            services.AddCors(opts => opts.AddPolicy("AllowAll", builder =>
             {
                 builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
             }));

            services.AddMvc();

            //services.AddIdentityServer()
            //    .AddDeveloperSigningCredential()
            //    .AddMongoRepository()
            //    .AddIdentityApiResources()
            //    .AddPersistedGrants()
            //    .AddClients()
            //    .AddTestUsers(IdentityConfig.GetUsers());

            services.AddIdentityServer()
                         .AddDeveloperSigningCredential()
                         .AddInMemoryApiResources(IdentityConfig.GetApiResources())
                         .AddInMemoryIdentityResources(IdentityConfig.GetIdentityResources())
                         .AddInMemoryClients(IdentityConfig.GetClients())
                         .AddTestUsers(IdentityConfig.GetUsers());
            services.AddAuthentication()
                .AddGoogle("Google", options =>
                {
                    options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    options.ClientId = Configuration["GoogleProvider:ClientId"];
                    options.ClientSecret = Configuration["GoogleProvider:ClientSecret"];
                })
                .AddFacebook(opts =>
                {
                    opts.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    opts.AppId = Configuration["FacebookProvider:AppId"];
                    opts.AppSecret = Configuration["FacebookProvider:AppSecret"];
                })
                .AddTwitter(opts =>
                {
                    opts.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                    opts.ConsumerKey = Configuration["TwitterProvider:ConsumerKey"];
                    opts.ConsumerSecret = Configuration["TwitterProvider:ConsumerSecret"];
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("AllowAll");

            app.UseIdentityServer();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }
    }
}
