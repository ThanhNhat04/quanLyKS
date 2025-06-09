
using HotelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace HotelManagement.Controllers
{
    public class LoginRequest
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
    }

    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IConfiguration _configuration;

        public AuthController(HotelDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Auth/Register
        [HttpPost("Register")]
        public IActionResult Register([FromBody] LoginRequest model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
                return BadRequest("Username and password are required.");

            if (_context.Accounts.Any(a => a.Username == model.Username))
                return BadRequest("Username already exists.");

            var account = new Account
            {
                Username = model.Username,
                Password = model.Password,
                DisplayName = model.Username,
                Email = "",
                Phone = "",
                RoleId = 3
            };

            _context.Accounts.Add(account);
            _context.SaveChanges();

            return Ok("Registration successful.");
        }

        // POST: api/Auth/Login
        [HttpPost("Login")]
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var account = _context.Accounts
                .FirstOrDefault(a => a.Username == model.Username && a.Password == model.Password);

            if (account == null)
                return Unauthorized("Invalid username or password.");

            var token = GenerateJwtToken(account.Username);
            return Ok(new
            {
                message = "Login successful",
                token = token
            });
        }

        private string GenerateJwtToken(string username)
        {
            var keyString = _configuration["Jwt:Key"];
            if (string.IsNullOrEmpty(keyString))
                throw new InvalidOperationException("JWT Key is missing in configuration.");

            var key = Encoding.UTF8.GetBytes(keyString);
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, username)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(3),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
