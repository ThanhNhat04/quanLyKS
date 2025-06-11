using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelManagement.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

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
        public async Task<ActionResult<IEnumerable<RoomDto>>> GetAll()
        {
            var rooms = await _context.Rooms
                .Include(r => r.RoomType)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType.RoomTypeName,
                    Price = r.Price,
                    Status = r.Status
                })
                .ToListAsync();

            return Ok(rooms);
        }

        // GET: api/Room/5
        [HttpGet("{roomId}")]
        public async Task<ActionResult<RoomDto>> GetById(int roomId)
        {
            var room = await _context.Rooms
                .Include(r => r.RoomType)
                .Where(r => r.RoomId == roomId)
                .Select(r => new RoomDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber,
                    RoomType = r.RoomType.RoomTypeName,
                    Price = r.Price,
                    Status = r.Status
                })
                .FirstOrDefaultAsync();

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

            // Kiểm tra RoomTypeId hợp lệ
            var exists = await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeId == model.RoomTypeId);
            if (!exists)
                return BadRequest($"RoomType with ID = {model.RoomTypeId} does not exist.");

            // Gán Bookings trống để tránh lỗi nếu bị null
            model.Bookings = new List<Booking>();

            _context.Rooms.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { roomId = model.RoomId }, model);
        }

        // PUT: api/Room/5
        [HttpPut("{roomId}")]
        public async Task<IActionResult> Update(int roomId, [FromBody] Room model)
        {
            if (roomId != model.RoomId)
                return BadRequest("Room ID mismatch.");

            var existingRoom = await _context.Rooms.FindAsync(roomId);
            if (existingRoom == null)
                return NotFound($"Room with ID = {roomId} not found.");

            var roomTypeExists = await _context.RoomTypes.AnyAsync(rt => rt.RoomTypeId == model.RoomTypeId);
            if (!roomTypeExists)
                return BadRequest($"RoomType with ID = {model.RoomTypeId} does not exist.");

            _context.Entry(existingRoom).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Room updated successfully.");
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

            return Ok("Room deleted successfully.");
        }
    }

    // ✅ DTO đơn giản hóa cho GET
    public class RoomDto
    {
        public int RoomId { get; set; }
        public string RoomNumber { get; set; }
        public string RoomType { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
    }
}
