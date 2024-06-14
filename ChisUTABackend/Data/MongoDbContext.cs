using System.Configuration;
using ChisUTABackend.Models;
using MongoDB.Driver;

namespace ChisUTABackend.Data
{
    public class MongoDbContext
    {

        private readonly IMongoDatabase _database;

        public MongoDbContext()
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDbConnection"].ConnectionString);
            _database = client.GetDatabase("Db_ChisUTA");
        }

        public IMongoCollection<Users> Users => _database.GetCollection<Users>("Users");
    }
}