using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.MVCWeb.Models.Services
{
    public class ProjectLink
    {
        public ProjectLink(string name, string url)
        {
            Name = name;
            Url = url;
        }

        public string Name { get; set; }
        public string Url { get; set; }
    }

}
