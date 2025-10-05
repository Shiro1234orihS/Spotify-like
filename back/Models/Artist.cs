using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SpotifyAPI.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("artist")]
        public string Artist { get; set; } = null!;

        [BsonElement("album")]
        public string Album { get; set; } = null!;

        [BsonElement("duration")]
        public int Duration { get; set; }

        [BsonElement("genre")]
        public string Genre { get; set; } = DateTime.UtcNow;

        [BsonElement("releaseDate")]
        public DateTime? ReleaseDate { get; set; }

        [BsonElement("audioUrl")]
        public string AudioUrl { get; set; } = false;

        [BsonElement("coverUrl")]
        public string CoverUrl { get; set; } = false;

        [BsonElement("createdAt")]
        public dataTime CreatedAt { get; set; } = false;
    }
}