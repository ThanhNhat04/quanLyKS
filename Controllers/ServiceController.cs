using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServiceController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ServiceController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Service
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var services = await _context.Services.ToListAsync();
            return Ok(services);
        }

        // GET: api/Service/5
        [HttpGet("{serviceId}")]
        public async Task<IActionResult> GetById(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return NotFound($"Service with ID = {serviceId} not found.");

            return Ok(service);
        }

        // POST: api/Service
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Service model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Services.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { serviceId = model.ServiceId }, model);
        }

        // PUT: api/Service/5
        [HttpPut("{serviceId}")]
        public async Task<IActionResult> Update(int serviceId, [FromBody] Service model)
        {
            if (serviceId != model.ServiceId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Services.FindAsync(serviceId);
            if (existing == null)
                return NotFound($"Service with ID = {serviceId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Service/5
        [HttpDelete("{serviceId}")]
        public async Task<IActionResult> Delete(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service == null)
                return NotFound($"Service with ID = {serviceId} not found.");

            _context.Services.Remove(service);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
