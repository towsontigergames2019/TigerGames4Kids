

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TigerGames4Kids.Models
{
    public class GameType
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Title")]
        public string Title { get; set; }

        [BsonElement("URI")]
        public string URI { get; set; }

        [BsonElement("Genre")]
        public string Genre { get; set; }
    }
}