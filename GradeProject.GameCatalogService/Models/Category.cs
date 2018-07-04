using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Models
{
    public class Category
    {
        [BsonId]
        public Guid Id { get; set; }

        public string Name { get; set; }
        public int Count { get; set; }

        public Category()
        {
            Id = Guid.NewGuid();
        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }
    }
}
