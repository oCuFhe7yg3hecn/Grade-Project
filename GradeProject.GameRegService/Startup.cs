using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using GradeProject.GameRegService.Communication;
using GradeProject.GameRegService.Infrstructure;
using GradeProject.GameRegService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GradeProject.GameRegService
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

            services.AddAutoMapper();

            var rs = new RabbitMqEventBus();

            services.Configure<RabbitBusOptions>(config => config.HostName = "");

            //REgister Dependencies
            AppContainer = RegisterDependencies(services);

            var rabbitMq = AppContainer.Resolve<IEventBus>();

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

        private IContainer RegisterDependencies(IServiceCollection services)
        {
            var builder = new ContainerBuilder();
            builder.Populate(services);

            //Utils

            builder.Register(c => new RabbitMqEventBus()).As<IEventBus>();

            //Services

            builder.Register(c => new RegistrationService(c.Resolve<IEventBus>(), c.Resolve<IMapper>())).As<IRegistrationService>();


            return builder.Build();
        }
    }
}
