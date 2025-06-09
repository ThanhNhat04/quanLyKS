using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookingController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public BookingController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Booking
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var bookings = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                .ToListAsync();

            return Ok(bookings);
        }

        // GET: api/Booking/5
        [HttpGet("{bookingId}")]
        public async Task<IActionResult> GetById(int bookingId)
        {
            var booking = await _context.Bookings
                .Include(b => b.Customer)
                .Include(b => b.Room)
                .FirstOrDefaultAsync(b => b.BookingId == bookingId);

            if (booking == null)
                return NotFound($"Booking with ID = {bookingId} not found.");

            return Ok(booking);
        }

        // POST: api/Booking
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Booking model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Bookings.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { bookingId = model.BookingId }, model);
        }

        // PUT: api/Booking/5
        [HttpPut("{bookingId}")]
        public async Task<IActionResult> Update(int bookingId, [FromBody] Booking model)
        {
            if (bookingId != model.BookingId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Bookings.FindAsync(bookingId);
            if (existing == null)
                return NotFound($"Booking with ID = {bookingId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Booking/5
        [HttpDelete("{bookingId}")]
        public async Task<IActionResult> Delete(int bookingId)
        {
            var booking = await _context.Bookings.FindAsync(bookingId);
            if (booking == null)
                return NotFound($"Booking with ID = {bookingId} not found.");

            _context.Bookings.Remove(booking);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
