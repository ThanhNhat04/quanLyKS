using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingServiceController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public BookingServiceController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/BookingService
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = await _context.BookingServices
                .Include(bs => bs.Booking)
                .Include(bs => bs.Service)
                .ToListAsync();
            return Ok(list);
        }

        // GET: api/BookingService/{bookingId}/{serviceId}
        [HttpGet("{bookingId}/{serviceId}")]
        public async Task<IActionResult> GetById(int bookingId, int serviceId)
        {
            var item = await _context.BookingServices
                .Include(bs => bs.Booking)
                .Include(bs => bs.Service)
                .FirstOrDefaultAsync(bs => bs.BookingId == bookingId && bs.ServiceId == serviceId);

            if (item == null)
                return NotFound("Not found.");

            return Ok(item);
        }

        // POST: api/BookingService
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookingService model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.BookingServices.Add(model);
            await _context.SaveChangesAsync();
            return Ok(model);
        }

        // PUT: api/BookingService/{bookingId}/{serviceId}
        [HttpPut("{bookingId}/{serviceId}")]
        public async Task<IActionResult> Update(int bookingId, int serviceId, [FromBody] BookingService model)
        {
            if (bookingId != model.BookingId || serviceId != model.ServiceId)
                return BadRequest("Composite key mismatch.");

            var existing = await _context.BookingServices.FindAsync(bookingId, serviceId);
            if (existing == null)
                return NotFound("Not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();
            return Ok("Update successful.");
        }

        // DELETE: api/BookingService/{bookingId}/{serviceId}
        [HttpDelete("{bookingId}/{serviceId}")]
        public async Task<IActionResult> Delete(int bookingId, int serviceId)
        {
            var item = await _context.BookingServices.FindAsync(bookingId, serviceId);
            if (item == null)
                return NotFound("Not found.");

            _context.BookingServices.Remove(item);
            await _context.SaveChangesAsync();
            return Ok("Deleted.");
        }
    }
}
