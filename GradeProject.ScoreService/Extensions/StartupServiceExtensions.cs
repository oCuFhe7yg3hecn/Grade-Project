using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Extensions
{
    public static class StartupServiceExtensions
    {
        public static void AddIdentityServerConfig(this AuthenticationBuilder authBuilder)
        {
            authBuilder.AddIdentityServerAuthentication(options =>
            {
                options.RequireHttpsMetadata = true;

                options.Authority = "https://localhost:44362";
                options.ApiName = "Platform.ProfileService";
            });
        }
    }
}
