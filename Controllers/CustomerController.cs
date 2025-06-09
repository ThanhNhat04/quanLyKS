using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
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
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers.ToListAsync();
            return Ok(customers);
        }

        // GET: api/Customer/5
        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetById(int customerId)
        {
            var customer = await _context.Customers.FindAsync(customerId);
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
}
