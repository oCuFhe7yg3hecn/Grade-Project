using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Models.DTO
{
    public class ProjectLink
    {
        public ProjectLink(string name, string url)
        {
            Name = name;
            Url = url;
        }

        [Key]
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
