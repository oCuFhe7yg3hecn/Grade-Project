using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Configurations
{
    public class RabbitMqConfig
    {
        public string HostName { get; set; }
        public string Exchange { get; set; }
        public string QueueRoutingKey { get; set; }
    }
}
