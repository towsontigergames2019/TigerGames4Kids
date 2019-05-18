using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TigerGames4Kids.Models
{
    public class RecordType
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("UserId")]
        public ObjectId UserId { get; set; }

        [BsonElement("GameId")]
        public ObjectId GameId { get; set; }

        [BsonElement("Timestamp")]
        public DateTime Timestamp { get; set; }
    }
}