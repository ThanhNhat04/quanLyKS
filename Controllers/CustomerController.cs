using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public CustomerController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Customer
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerDto>>> GetAll()
        {
            var customers = await _context.Customers
                .Include(c => c.Account)
                .Include(c => c.Role)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Username = c.Account.Username,
                    RoleName = c.Role.RoleName
                })
                .ToListAsync();

            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{customerId}")]
        public async Task<ActionResult<CustomerDto>> GetById(int customerId)
        {
            var customer = await _context.Customers
                .Include(c => c.Account)
                .Include(c => c.Role)
                .Where(c => c.CustomerId == customerId)
                .Select(c => new CustomerDto
                {
                    CustomerId = c.CustomerId,
                    FullName = c.FullName,
                    Email = c.Email,
                    PhoneNumber = c.PhoneNumber,
                    Address = c.Address,
                    Username = c.Account.Username,
                    RoleName = c.Role.RoleName
                })
                .FirstOrDefaultAsync();

            if (customer == null)
                return NotFound($"Customer with ID = {customerId} not found.");

            return Ok(customer);
        }

        // POST: api/Customer
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Kiểm tra AccountId và RoleId có tồn tại
            if (!await _context.Accounts.AnyAsync(a => a.AccountId == model.AccountId))
                return BadRequest($"Account with ID = {model.AccountId} not found.");
            if (!await _context.Roles.AnyAsync(r => r.RoleId == model.RoleId))
                return BadRequest($"Role with ID = {model.RoleId} not found.");

            _context.Customers.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { customerId = model.CustomerId }, model);
        }

        // PUT: api/Customer/5
        [HttpPut("{customerId}")]
        public async Task<IActionResult> Update(int customerId, [FromBody] Customer model)
        {
            if (customerId != model.CustomerId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Customers.FindAsync(customerId);
            if (existing == null)
                return NotFound($"Customer with ID = {customerId} not found.");

            // Kiểm tra AccountId và RoleId có tồn tại
            if (!await _context.Accounts.AnyAsync(a => a.AccountId == model.AccountId))
                return BadRequest($"Account with ID = {model.AccountId} not found.");
            if (!await _context.Roles.AnyAsync(r => r.RoleId == model.RoleId))
                return BadRequest($"Role with ID = {model.RoleId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Customer/5
        [HttpDelete("{customerId}")]
        public async Task<IActionResult> Delete(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
            if (customer == null)
                return NotFound($"Customer with ID = {customerId} not found.");

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }

    // DTO cho GET
    public class CustomerDto
    {
        public int CustomerId { get; set; }
        public string? FullName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? RoleName { get; set; }
    }
}
