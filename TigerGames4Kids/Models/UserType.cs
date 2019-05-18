using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TigerGames4Kids.Models
{
    public class UserType
    {
        [BsonId]
        public ObjectId Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Username")]
        public string Username { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; }

        [BsonElement("Password")]
        public string Password { get; set; }

        [BsonElement("ProfileImageURI")]
        public string ProfileImageURI { get; set; }

        [BsonElement("Role")]
        public string Role { get; set; }

        [BsonElement("ParentEmail")]
        public string ParentEmail { get; set; }
    }
}
