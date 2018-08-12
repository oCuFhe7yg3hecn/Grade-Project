using GradeProject.ScoreService.Infrastructure.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Models
{
    [CollectionName("Scores")]
    public class Score
    {
        public Score()
        {
            Id = Guid.NewGuid();
        }

        [BsonId]
        public Guid Id { get; set; }

        public string Game { get; set; }
        public Guid UserId { get; set; }
        public float Value { get; set; }
    }
}
