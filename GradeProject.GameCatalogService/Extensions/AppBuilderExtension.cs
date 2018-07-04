using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Extensions
{
    public static class AppBuilderExtension
    {
        public static void UseOData(this IApplicationBuilder app)
        {
            app.UseMvc(routeBuilder =>
            {
            });
        }
    }
}
