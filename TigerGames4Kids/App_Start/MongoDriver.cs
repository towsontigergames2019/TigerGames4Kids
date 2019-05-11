using System;
using System.Configuration;
using MongoDB.Driver;

namespace TigerGames4Kids.App_Start 
{   
    public class MongoDriver 
    {
        MongoClient _client;
        MongoServer _server;
        public MongoDatabase _database;

        public MongoDriver()
        {
            var MongoDataBaseName = ConfigurationManager.AppSettings["MongoDatabaseName"];
            var MongoUsername = ConfigurationManager.AppSettings["MongoUsername"];
            var MongoPassword = ConfigurationManager.AppSettings["MongoPassword"];
            var MongoPort = ConfigurationManager.AppSettings["MongoPort"];
            var MongoHost = ConfigurationManager.AppSettings["MongoHost"];

            var credential = MongoCredential.CreateMongoCRCredential(MongoDataBaseName, MongoUsername, MongoPassword);

            var settings = new MongoClientSettings
            {
                Credentials = new[] { credential },
                Server = new MongoServerAddress(MongoHost, Convert.ToInt32(MongoPort))
            };
            _client = new MongoClient(settings);
            _server = _client.GetServer();
            _database = _server.GetDatabase(MongoDataBaseName);
        }
    }
}

