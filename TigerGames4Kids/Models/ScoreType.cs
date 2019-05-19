using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TigerGames4Kids.Models
{
    public class ScoreType : Controller
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Username")]
        public String Username { get; set; }

        [BsonElement("GameTitle")]
        public String GameTitle { get; set; }

        [BsonElement("Score")]
        public String Score { get; set; }
    }
}
