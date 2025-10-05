// Models/Song.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace back.Models
{
    public class Song
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("title")]
        public string Title { get; set; } = null!;

        [BsonElement("artist")]
        public string Artist { get; set; } = null!;

        [BsonElement("artistId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ArtistId { get; set; }

        [BsonElement("album")]
        public string? Album { get; set; }

        [BsonElement("duration")]
        public int Duration { get; set; }

        [BsonElement("genre")]
        public string? Genre { get; set; }

        [BsonElement("releaseDate")]
        public DateTime? ReleaseDate { get; set; }

        [BsonElement("audioUrl")]
        public string AudioUrl { get; set; } = null!;

        [BsonElement("coverUrl")]
        public string? CoverUrl { get; set; }

        [BsonElement("plays")]
        public long Plays { get; set; } = 0;

        [BsonElement("likes")]
        public long Likes { get; set; } = 0;

        [BsonElement("createdAt")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

