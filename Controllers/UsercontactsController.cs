using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebPlantApi.Models;

namespace WebPlantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsercontactsController : ControllerBase
    {
        private readonly PlantDbContext _context;

        public UsercontactsController(PlantDbContext context)
        {
            _context = context;
        }

        // GET: api/usercontacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usercontact>>> GetUsercontacts()
        {
            return await _context.Usercontacts.ToListAsync();
        }

        // GET: api/usercontacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Usercontact>> GetUsercontact(int id)
        {
            var usercontact = await _context.Usercontacts.FindAsync(id);

            if (usercontact == null)
            {
                return NotFound();
            }

            return usercontact;
        }

        // PUT: api/usercontacts/5
        // Cập nhật thông tin user contact, không cần gửi id trong body
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUsercontact(int id, [FromBody] Usercontact usercontact)
        {
            // Tìm kiếm user contact có id từ URL
            var existingUsercontact = await _context.Usercontacts.FindAsync(id);

            // Nếu không tìm thấy user contact, trả về NotFound
            if (existingUsercontact == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin của user contact
            existingUsercontact.Contacttype = usercontact.Contacttype;
            existingUsercontact.Contactvalue = usercontact.Contactvalue;
            existingUsercontact.Isprimary = usercontact.Isprimary;
            // Không cập nhật Createdat, vì đây là thông tin tự động tạo khi tạo mới
            existingUsercontact.Createdat = usercontact.Createdat;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Trả về kết quả thành công
            return NoContent();
        }

        // POST: api/usercontacts
        // Tạo mới một user contact, không cần gửi id trong body
        [HttpPost]
        public async Task<ActionResult<Usercontact>> PostUsercontact([FromBody] Usercontact usercontact)
        {
            // Không cần id trong body, tự động tạo giá trị cho id
            usercontact.Createdat = DateTime.UtcNow; // Gán CreatedAt khi tạo user contact mới
            _context.Usercontacts.Add(usercontact);
            await _context.SaveChangesAsync();

            // Trả về thông tin user contact vừa tạo, bao gồm id mới
            return CreatedAtAction("GetUsercontact", new { id = usercontact.Id }, usercontact);
        }

        // DELETE: api/usercontacts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsercontact(int id)
        {
            var usercontact = await _context.Usercontacts.FindAsync(id);
            if (usercontact == null)
            {
                return NotFound();
            }

            _context.Usercontacts.Remove(usercontact);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsercontactExists(int id)
        {
            return _context.Usercontacts.Any(e => e.Id == id);
        }
    }
}
