using ChisUTABackend.Data;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace ChisUTABackend.Services
{
    public class BaseServices
    {
        public MongoDbContext _context;
        public readonly IMongoDatabase _database;

        public BaseServices()
        {
            var client = new MongoClient(ConfigurationManager.ConnectionStrings["MongoDbConnection"].ConnectionString);
            _database = client.GetDatabase("Db_ChisUTA");
        }
    }
}