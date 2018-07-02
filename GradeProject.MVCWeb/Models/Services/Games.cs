using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.MVCWeb.Models.Services
{
    public class Games
    {
        public class GameDTO
        {
            public GameDTO()
            {
                Id = Guid.NewGuid();
                Tags = new List<string>();
                Categories = new List<string>();
                MultiMedias = new List<string>();

                CreatedAt = DateTime.Now;
                RegistereAt = DateTime.Now;
                ProjectLinks = new Dictionary<string, string>();
                AvaliablePlatforms = new List<string>();
            }

            public Guid Id { get; set; }

            public string Name { get; set; }

            [DataType(DataType.MultilineText)]
            public string Description { get; set; }

            public string Version { get; set; }

            public List<string> Categories { get; set; }

            public List<string> Tags { get; set; }

            [DataType(DataType.ImageUrl)]
            public string CoverImageURL { get; set; }
            public List<string> MultiMedias { get; set; }

            [DataType(DataType.Url)]
            public string GameUrl { get; set; }

            public string Authority { get; set; }

            [DataType(DataType.Date)]
            public DateTime CreatedAt { get; set; }

            [DataType(DataType.DateTime)]
            public DateTime RegistereAt { get; set; }

            public int PlayersCount { get; set; }

            public Dictionary<string, string> ProjectLinks { get; set; }
            public List<string> AvaliablePlatforms { get; set; }
        }
    }
}
