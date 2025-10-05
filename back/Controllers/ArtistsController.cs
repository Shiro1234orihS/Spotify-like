using Microsoft.AspNetCore.Mvc;
using back.Models;
using back.Services;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistService _artistService;

        public ArtistsController(IArtistService artistService)
        {
            _artistService = artistService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Artist>>> GetAll()
        {
            var artists = await _artistService.GetAllAsync();
            return Ok(artists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Artist>> GetById(string id)
        {
            var artist = await _artistService.GetByIdAsync(id);
            if (artist == null)
                return NotFound();
            return Ok(artist);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<Artist>>> Search([FromQuery] string q)
        {
            var artists = await _artistService.SearchAsync(q);
            return Ok(artists);
        }

        [HttpPost]
        public async Task<ActionResult<Artist>> Create(Artist artist)
        {
            await _artistService.AddAsync(artist);
            return CreatedAtAction(nameof(GetById), new { id = artist.Id }, artist);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Artist artist)
        {
            var existing = await _artistService.GetByIdAsync(id);
            if (existing == null)
                return NotFound();

            artist.Id = id;
            await _artistService.UpdateAsync(id, artist);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var artist = await _artistService.GetByIdAsync(id);
            if (artist == null)
                return NotFound();

            await _artistService.DeleteAsync(id);
            return NoContent();
        }
    }
}
