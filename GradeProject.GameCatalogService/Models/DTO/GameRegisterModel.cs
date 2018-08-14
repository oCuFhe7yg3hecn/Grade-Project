using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Models
{
    public class GameRegisterModel
    {
        public string Name { get; set; }

        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        public string Version { get; set; }

        public List<string> Categories { get; set; }

        public List<string> Tags { get; set; }

        public IFormFile CoverImageUrl { get; set; }

        public List<IFormFile> MultimediaFiles { get; set; }

        [DataType(DataType.Url)]
        public string GameUrl { get; set; }

        public string Authority { get; set; }

        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public Dictionary<string, string> ProjectLinks { get; set; }

        public List<string> AvaliablePlatforms { get; set; }
    }
}
