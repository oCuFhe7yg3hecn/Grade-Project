using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Extensions
{
    public static class API
    {
        public static class Profiles
        {
            public static string RegisterProfile(string basePath) => $"{basePath}/api/Players";
        }
    }
}
