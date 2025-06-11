using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using HotelManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly HotelDbContext _context;
        private readonly IConfiguration _configuration;

        public AccountController(HotelDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        // POST: api/Account/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest model)
        {
            if (await _context.Accounts.AnyAsync(a => a.Username == model.Username))
            {
                return BadRequest("Username already exists.");
            }

            var account = new Account
            {
                Username = model.Username,
                Password = model.Password,
                DisplayName = model.DisplayName,
                Email = model.Email,
                Phone = model.Phone
            };

            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();

            var customer = new Customer
            {
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.Phone,
                Address = model.Address,
                AccountId = account.AccountId,
                RoleId = 1 // Mặc định là Quản lý
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            return Ok(new { message = "Registration successful", AccountId = account.AccountId });
        }

        // POST: api/Account/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            var account = await _context.Accounts
                .Include(a => a.Customer)
                .ThenInclude(c => c.Role)
                .FirstOrDefaultAsync(a => a.Username == model.Username && a.Password == model.Password);

            if (account == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var token = GenerateJwtToken(account);

            return Ok(new
            {
                token,
                account.AccountId,
                account.Username,
                DisplayName = account.DisplayName,
                Role = account.Customer?.Role?.RoleName ?? "Unknown"
            });
        }

        // Generate JWT token
        private string GenerateJwtToken(Account account)
        {
            var role = account.Customer?.Role?.RoleName ?? "User";

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, account.AccountId.ToString()),
                new Claim(ClaimTypes.Name, account.Username),
                new Claim(ClaimTypes.Role, role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }

    // DTOs

    public class RegisterRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public string FullName { get; set; }
        public string Address { get; set; }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
