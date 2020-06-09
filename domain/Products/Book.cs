using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Products {
    public class Book : IProduct {
        [BsonId]
        [BsonRepresentation (BsonType.ObjectId)]
        public string Id { get; set; }

        public Category Category { get; set; }

        public string Name { get; set; }

        public string Author { get; set; }

        public decimal Price { get; set; }

    }
}