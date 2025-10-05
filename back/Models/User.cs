using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = null!;

        [BsonElement("email")]
        public string Email { get; set; } = null!;

        [BsonElement("displayName")]
        public string DisplayName { get; set; } = null!;

        [BsonElement("avatarUrl")]
        public string? AvatarUrl { get; set; }

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [BsonElement("lastLogin")]
        public DateTime? LastLogin { get; set; }

        [BsonElement("isPremium")]
        public bool IsPremium { get; set; } = false;
    }
}