using GradeProject.GameCatalogService.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.GameCatalogService.Infrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public IMongoCollection<GameInfo> Games => _database.GetCollection<GameInfo>("GamesData");

        public IMongoCollection<Category> Categories => _database.GetCollection<Category>("Categories");

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null) { _database = client.GetDatabase(settings.Value.Database); }
            Seed(_database);
        }

        public void Seed(IMongoDatabase _db)
        {
            var gamesColl = _db.GetCollection<GameInfo>("GamesData");
            if (gamesColl.Count(new BsonDocument()) == 0)
            {
                gamesColl.InsertMany(new[]{
                    new GameInfo()
                    {
                        Name = "My Pacman Game",
                        Description = "This is my base pacman Game with lots of intersting stuff. Youc can play as \n" +
                                      "Here you can see that this is almost like a modern games with lots of players.Try \n" +
                                      "it out an you'll be pleasured with agreat highscore. Invite some friends to get best \n" +
                                      "in-game experince and do some interst.",
                        CoverImageURL = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTp7z6RGnIrSeraUEispZlT_b7cukpX9Qp3HhAKBonxsYQIEQr_",
                        GameUrl = "https://localhost:44300",
                        MultiMedias = new List<string>(){ "", "", "" },
                        ProjectLinks = new Dictionary<string, string>(
                            new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("Official Site", "https://localhost:44300/Home"),
                                new KeyValuePair<string, string>("Community", "https://localhost:44300/Community"),
                                new KeyValuePair<string, string>("Forum", "https://localhost:44300/Forum"),
                                new KeyValuePair<string, string>("Contact", "https://localhost:44300/Contact"),
                            }),
                        PlayersCount = 3175,
                        Authority = "Solar Game Industry",
                        Tags = new List<string>(){ "Arcade", "Action", "MMO", "Innovative", "Comming Popular"},
                        Version = "v 1.3",
                        AvaliablePlatforms = new List<string>(){"Web", "Crossplatform"}
                    },

                    new GameInfo()
                    {
                        Name = "My Interesting Game",
                        Description = "This is my Interesting Game with lots of intersting stuff. Youc can play as \n" +
                                      "Here you can see that this is almost like a modern games with lots of players.Try \n" +
                                      "it out an you'll be pleasured with agreat highscore. Invite some friends to get best \n" +
                                      "in-game experince and do some interst.",
                        CoverImageURL = "https://cdn.theatlantic.com/assets/media/img/photo/2015/11/images-from-the-2016-sony-world-pho/s01_130921474920553591/main_900.jpg?1448476701",
                        GameUrl = "https://localhost:44300",
                        MultiMedias = new List<string>(){ "", "", "" },
                        ProjectLinks = new Dictionary<string, string>(
                            new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("Official Site", "https://localhost:44300/Home"),
                                new KeyValuePair<string, string>("Community", "https://localhost:44300/Community"),
                                new KeyValuePair<string, string>("Forum", "https://localhost:44300/Forum"),
                                new KeyValuePair<string, string>("Contact", "https://localhost:44300/Contact"),
                            }),
                        PlayersCount = 3175,
                        Authority = "Ground Squirel Company",
                        Tags = new List<string>(){ "Arcade", "Action", "Runner", "CrossPlatform", "Third-person"},
                        Version = "v 2.25",
                        AvaliablePlatforms = new List<string>(){"Windows", "Linux"}
                    },

                    new GameInfo()
                    {
                        Name = "Some Third Game",
                        Description = "This is some third hame with lots of intersting stuff. Youc can play as \n" +
                                      "Here you can see that this is almost like a modern games with lots of players.Try \n" +
                                      "it out an you'll be pleasured with agreat highscore. Invite some friends to get best \n" +
                                      "in-game experince and do some interst.",
                        CoverImageURL = "https://www.worldatlas.com/r/w728-h425-c728x425/upload/44/03/55/shutterstock-73764937.jpg",
                        GameUrl = "https://localhost:44300",
                        MultiMedias = new List<string>(){ "", "", "" },
                        ProjectLinks = new Dictionary<string, string>(
                            new List<KeyValuePair<string, string>>() {
                                new KeyValuePair<string, string>("Official Site", "https://localhost:44300/Home"),
                                new KeyValuePair<string, string>("Community", "https://localhost:44300/Community"),
                                new KeyValuePair<string, string>("Forum", "https://localhost:44300/Forum"),
                                new KeyValuePair<string, string>("Contact", "https://localhost:44300/Contact"),
                            }),
                        PlayersCount = 3175,
                        Authority = "Forest Comp",
                        Tags = new List<string>(){ "Arcade", "Action", "Runner", "CrossPlatform", "Squirell Games", "Animal"},
                        Version = "v 3.33",
                        AvaliablePlatforms = new List<string>(){"Crossplatform"}
                    }
                });
            }
        }

    }
}
