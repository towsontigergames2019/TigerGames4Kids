using System;
using System.Configuration;
using MongoDB.Driver;

namespace TigerGames4Kids.App_Start 
{   
    public class MongoDriver 
    {
        MongoClient _client;
        public IMongoDatabase _database;

        public MongoDriver()
        {
            _client = new MongoClient("mongodb://admin:Password1@ds155516.mlab.com:55516/tigergames4kids");
            _database = _client.GetDatabase("tigergames4kids");
        }
    }
}

