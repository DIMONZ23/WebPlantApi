using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebPlantApi.Models;

namespace WebPlantApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly PlantDbContext _context;

        public AuthController(IConfiguration configuration, PlantDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginModel userLogin)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == userLogin.Username && u.Passwordhash == userLogin.Passwordhash);
            if (user == null)
            {
                return Unauthorized("Invalid credentials");
            }

            // Tạo danh sách Claims
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, "User") // Bạn có thể thay đổi thành nhiều vai trò khác nhau
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30), // Đặt thời gian hết hạn cho token
                signingCredentials: credentials
            );

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.WriteToken(token);

            return Ok(new { Token = jwtToken });
        }
    }

    public class UserLoginModel
    {
        public string Username { get; set; }
        public string Passwordhash { get; set; }
    }
}
