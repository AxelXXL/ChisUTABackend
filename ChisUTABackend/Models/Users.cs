using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;


namespace ChisUTABackend.Models
{
    public partial class Users
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set;}

        [BsonElement("email")]
        public string Email { get; set;}

        [BsonElement("password")]
        public string Password { get; set;}
    }
}