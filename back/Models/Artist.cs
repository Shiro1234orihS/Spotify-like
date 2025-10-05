using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back.Models
{
    public class Artist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; } = null!;

        [BsonElement("bio")]
        public string? Bio { get; set; }

        [BsonElement("country")]
        public string? Country { get; set; }

        [BsonElement("genre")]
        public string? Genre { get; set; }

        [BsonElement("imageUrl")]
        public string? ImageUrl { get; set; }

        [BsonElement("verified")]
        public bool Verified { get; set; } = false;

        [BsonElement("monthlyListeners")]
        public int MonthlyListeners { get; set; } = 0;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("updatedAt")]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
