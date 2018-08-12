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

            //Initialize();
        }


        private void Initialize()
        {
            var coll = GetClollection<Score>("Scores");
            coll.InsertMany(new List<Score>() {
                new Score()
                {
                    UserId = "bfe28474-8297-4638-bcc9-14a7f3a9a7d3",
                    Game = "Game1",
                    Value = 25.04f
                },
                new Score()
                {
                    UserId = "bfe28474-8297-4638-bcc9-14a7f3a9a7d3",
                    Game = "Game2",
                    Value  = 25.04f
                },
                new Score()
                {
                    UserId = "bfe28474-8297-4638-bcc9-14a7f3a9a7d3",
                    Game = "Game3",
                    Value  = 25.04f
                },
                new Score()
                {
                    UserId = "bfe28474-8297-4638-bcc9-14a7f3a9a7d3",
                    Game = "Game4",
                    Value = 25.04f
                }
            });
        }

        public IMongoCollection<T> GetClollection<T>(string collectionName) => _database.GetCollection<T>(collectionName);

    }
}
