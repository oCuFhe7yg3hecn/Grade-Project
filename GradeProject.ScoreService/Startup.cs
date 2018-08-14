using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using GradeProject.ScoreService.Extensions;
using GradeProject.ScoreService.Infrastructure;
using GradeProject.ScoreService.Infrastructure.Repos;
using GradeProject.ScoreService.Models;
using GradeProject.ScoreService.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDbSettings"));

            services
                .AddAuthentication("Bearer")
                .AddIdentityServerAuthentication();

            services.AddAppCors();

            services.AddAutoMapper();

            services.AddScoped(typeof(MongoDbContext));
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepo<>));
            services.AddScoped<IScoreService, Services.ScoreService>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
