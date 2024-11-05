using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPlantApi.Models;

namespace WebPlantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItermPlantsController : ControllerBase
    {
        private readonly PlantDbContext _context;

        public ItermPlantsController(PlantDbContext context)
        {
            _context = context;
        }

        // GET: api/plant
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlantIterms>>> GetPlants()
        {
            return await _context.PlantItems.ToListAsync();
        }
        // POST api/plant
        [HttpPost]
        public async Task<IActionResult> CreatePlant([FromBody] PlantIterms plantItem)
        {
            if (plantItem == null)
            {
                return BadRequest("Invalid plant item.");
            }

            // Thêm đối tượng mới vào DbSet mà không cần chỉ định ID
            _context.PlantItems.Add(plantItem);

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(CreatePlant), new { id = plantItem.ID }, plantItem);
        }
    }
}
