using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Room
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rooms = await _context.Rooms.ToListAsync();
            return Ok(rooms);
        }

        // GET: api/Room/5
        [HttpGet("{roomId}")]
        public async Task<IActionResult> GetById(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
                return NotFound($"Room with ID = {roomId} not found.");

            return Ok(room);
        }

        // POST: api/Room
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Room model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Rooms.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { roomId = model.RoomId }, model);
        }

        // PUT: api/Room/5
        [HttpPut("{roomId}")]
        public async Task<IActionResult> Update(int roomId, [FromBody] Room model)
        {
            if (roomId != model.RoomId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Rooms.FindAsync(roomId);
            if (existing == null)
                return NotFound($"Room with ID = {roomId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Room/5
        [HttpDelete("{roomId}")]
        public async Task<IActionResult> Delete(int roomId)
        {
            var room = await _context.Rooms.FindAsync(roomId);
            if (room == null)
                return NotFound($"Room with ID = {roomId} not found.");

            _context.Rooms.Remove(room);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
