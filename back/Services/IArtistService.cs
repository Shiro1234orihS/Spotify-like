using back.Models;

namespace back.Services
{
    public interface IArtistService
    {
        Task<List<Artist>> GetAllAsync();
        Task<Artist?> GetByIdAsync(string id);
        Task<Artist?> GetByNameAsync(string name);
        Task<List<Artist>> SearchAsync(string query);
        Task<List<Artist>> GetByGenreAsync(string genre);
        Task<List<Artist>> GetVerifiedArtistsAsync();
        Task<List<Artist>> GetTopArtistsAsync(int limit = 10);
        Task AddAsync(Artist artist);
        Task UpdateAsync(string id, Artist artist);
        Task DeleteAsync(string id);
        Task<bool> ExistsAsync(string id);
        Task<long> GetCountAsync();
    }
}
