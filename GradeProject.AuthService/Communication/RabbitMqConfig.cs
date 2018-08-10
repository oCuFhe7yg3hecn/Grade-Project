using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.Communication
{
    public static class RabbitMqConfig
    {
        public static string HostName { get; set; }
        public static string RegisterProfileExchange { get; set; }

    }
}
