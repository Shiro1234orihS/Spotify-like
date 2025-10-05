using MongoDB.Driver;
using Microsoft.Extensions.Options;
using back.Models;
using back.Configuration;

namespace back.Services
{
    public class ArtistService : IArtistService
    {
        private readonly IMongoCollection<Artist> _artistsCollection;

        public ArtistService(IOptions<MongoDbSettings> settings, MongoDbService mongoDbService)
        {
            _artistsCollection = mongoDbService.GetCollection<Artist>(settings.Value.ArtistsCollectionName);
        }

        public async Task<List<Artist>> GetAllAsync()
        {
            return await _artistsCollection.Find(_ => true).ToListAsync();
        }

        public async Task<Artist?> GetByIdAsync(string id)
        {
            return await _artistsCollection.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Artist?> GetByNameAsync(string name)
        {
            return await _artistsCollection.Find(a => a.Name == name).FirstOrDefaultAsync();
        }

        public async Task<List<Artist>> SearchAsync(string query)
        {
            var filter = Builders<Artist>.Filter.Or(
                Builders<Artist>.Filter.Regex(a => a.Name, new MongoDB.Bson.BsonRegularExpression(query, "i")),
                Builders<Artist>.Filter.Regex(a => a.Genre, new MongoDB.Bson.BsonRegularExpression(query, "i"))
            );
            return await _artistsCollection.Find(filter).ToListAsync();
        }

        public async Task<List<Artist>> GetByGenreAsync(string genre)
        {
            return await _artistsCollection.Find(a => a.Genre == genre).ToListAsync();
        }

        public async Task<List<Artist>> GetVerifiedArtistsAsync()
        {
            return await _artistsCollection.Find(a => a.Verified == true).ToListAsync();
        }

        public async Task<List<Artist>> GetTopArtistsAsync(int limit = 10)
        {
            return await _artistsCollection
                .Find(_ => true)
                .SortByDescending(a => a.MonthlyListeners)
                .Limit(limit)
                .ToListAsync();
        }

        public async Task AddAsync(Artist artist)
        {
            artist.CreatedAt = DateTime.UtcNow;
            artist.UpdatedAt = DateTime.UtcNow;
            await _artistsCollection.InsertOneAsync(artist);
        }

        public async Task UpdateAsync(string id, Artist artist)
        {
            artist.UpdatedAt = DateTime.UtcNow;
            await _artistsCollection.ReplaceOneAsync(a => a.Id == id, artist);
        }

        public async Task DeleteAsync(string id)
        {
            await _artistsCollection.DeleteOneAsync(a => a.Id == id);
        }

        public async Task<bool> ExistsAsync(string id)
        {
            var count = await _artistsCollection.CountDocumentsAsync(a => a.Id == id);
            return count > 0;
        }

        public async Task<long> GetCountAsync()
        {
            return await _artistsCollection.CountDocumentsAsync(_ => true);
        }
    }
}
