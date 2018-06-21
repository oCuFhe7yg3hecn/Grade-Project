using IdentityServer4.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GradeProject.AuthService.MongoInfrastructure
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public IMongoCollection<Client> Clients => _database.GetCollection<Client>("Client");

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null) { _database = client.GetDatabase(settings.Value.Database); }
        }
    }
}
