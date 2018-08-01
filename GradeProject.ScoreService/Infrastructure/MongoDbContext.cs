using GradeProject.ScoreService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.ScoreService.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null) { _database = client.GetDatabase(settings.Value.Database); }
        }

        public IMongoCollection<T> GetClollection<T>(string collectionName) => _database.GetCollection<T>(collectionName);

    }
}
