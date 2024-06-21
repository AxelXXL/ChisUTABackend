using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System.Collections.Generic;


namespace ChisUTABackend.Models
{
    public class ChismeModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("titulo")]
        public string Titulo { get; set; }

        [BsonElement("contexto")]
        public string Contexto { get; set; }

        [BsonElement("categoria")]
        public List<string> Categorias { get; set; }
    }
}