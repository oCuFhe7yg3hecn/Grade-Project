﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.AuthService.Communication;
using GradeProject.AuthService.Extensions;
using GradeProject.AuthService.Infrastructure;
using GradeProject.AuthService.Infrastructure.Clients;
using GradeProject.AuthService.MongoInfrastructure;
using GradeProject.AuthService.Services;
using IdentityServer4;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
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
            services.AddMvc();

            RegisterServices(services);

            RabbitMqConfig.HostName = Configuration["RabbitMqConfig:HostName"];
            RabbitMqConfig.RegisterProfileExchange = Configuration["RabbitMqConfig:RegisterProfileExchange"];

            services.Configure<MongoDbSettings>(opts =>
            {
                opts.Database = Configuration["MongoDbSettings:Database"];
                opts.ConnectionString = Configuration["MongoDbSettings:ConnectionString"];
            });

            services.AddDbContext<UsersContext>(opts =>
            {
                opts.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddAutoMapper();

            // Idnetity Server  Register
            services.AddIdentityService(Configuration);

            services.AddAuthorization(config =>
            {
                config.AddPolicy("DevelopersOnly", builder => builder.RequireRole("Developer").Build());
            });

            // External Providers
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

            // Cors
            services.AddCors(opts => opts.AddPolicy("AllowAll", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
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

            //app.UseAuthorization();

            app.UseStaticFiles();

            app.UseMvcWithDefaultRoute();
        }

        public void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<MongoDbSettings>();
            services.AddTransient<MongoDbContext>();

            services.AddScoped<UsersContext>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IClientService, ClientService>();

            services.AddScoped<IEventBus, RabbitMqBus>();
            services.AddScoped<IApiManagmentService, ApiManagmentService>();
            services.AddScoped<IFilesSaveService, FileSaveService>();
            services.AddScoped<IPlayerService, PlayerService>();
        }
    }
}
