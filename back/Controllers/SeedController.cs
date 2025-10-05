using Microsoft.AspNetCore.Mvc;
using back.Services;

namespace back.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SeedController : ControllerBase
    {
        private readonly DatabaseSeeder _seeder;

        public SeedController(DatabaseSeeder seeder)
        {
            _seeder = seeder;
        }

        [HttpPost("populate")]
        public async Task<IActionResult> PopulateDatabase()
        {
            try
            {
                await _seeder.SeedAsync();
                return Ok(new { success = true, message = "✅ Base remplie" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearDatabase()
        {
            try
            {
                await _seeder.ClearDatabaseAsync();
                return Ok(new { success = true, message = "✅ Base vidée" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }

        [HttpPost("reset")]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                await _seeder.ClearDatabaseAsync();
                await _seeder.SeedAsync();
                return Ok(new { success = true, message = "✅ Base réinitialisée" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, error = ex.Message });
            }
        }
    }
}