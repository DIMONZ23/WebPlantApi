﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPlantApi.Models;

namespace WebPlantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]  // Yêu cầu xác thực người dùng bằng token JWT
    public class PlantsController : ControllerBase
    {
        private readonly PlantDbContext _context;

        public PlantsController(PlantDbContext context)
        {
            _context = context;
        }

        // GET: api/plants
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Plant>>> GetPlants()
        {
            return await _context.Plants.ToListAsync();
        }

        // GET: api/plants/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Plant>> GetPlant(int id)
        {
            var plant = await _context.Plants.FindAsync(id);

            if (plant == null)
            {
                return NotFound();
            }

            return plant;
        }

        // PUT: api/plants/5
        // Cập nhật thông tin cây, không cần gửi id và createdat
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePlant(int id, [FromBody] Plant plant)
        {
            // Tìm kiếm cây có id từ URL
            var existingPlant = await _context.Plants.FindAsync(id);

            // Nếu không tìm thấy cây, trả về NotFound
            if (existingPlant == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin của cây
            existingPlant.Name = plant.Name;
            existingPlant.Imageurl = plant.Imageurl;
            existingPlant.Shortdescription = plant.Shortdescription;
            existingPlant.Detaileddescription = plant.Detaileddescription;
            existingPlant.Price = plant.Price;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Trả về kết quả thành công
            return NoContent();
        }


        // POST: api/plants
        // Tạo mới một cây, không cần gửi id và createdat
        [HttpPost]
        public async Task<ActionResult<Plant>> PostPlant(Plant plant)
        {
            // Kiểm tra ID tối đa hiện tại trong cơ sở dữ liệu và gán ID cho cây mới
            int nextId = _context.Plants.Any() ? _context.Plants.Max(p => p.Id) + 1 : 1;
            plant.Id = nextId; // Gán ID mới cho cây

            // Gán CreatedAt khi tạo cây mới
            plant.Createdat = DateTime.UtcNow;

            // Thêm cây vào cơ sở dữ liệu
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            // Trả về thông tin cây vừa tạo, bao gồm ID mới
            return CreatedAtAction("GetPlant", new { id = plant.Id }, plant);
        }


        // DELETE: api/plants/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlant(int id)
        {
            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }

            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlantExists(int id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}
