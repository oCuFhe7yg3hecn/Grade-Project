using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace GradeProject.ScoreService.Domain
{
    public class BaseEntity
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}
