using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameRegService.Models
{
    public class GameRegInfo
    {
        public GameRegInfo()
        {
            RegDate = DateTime.Now;
        }

        public string GameName { get; set; }
        public string Authority { get; set; }
        public DateTime RegDate { get; set; }
        public string Status { get; set; }
    }
}
