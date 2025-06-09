using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomTypeController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoomTypeController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/RoomType
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roomTypes = await _context.RoomTypes.ToListAsync();
            return Ok(roomTypes);
        }

        // GET: api/RoomType/5
        [HttpGet("{roomTypeId}")]
        public async Task<IActionResult> GetById(int roomTypeId)
        {
            var roomType = await _context.RoomTypes.FindAsync(roomTypeId);
            if (roomType == null)
                return NotFound($"RoomType with ID = {roomTypeId} not found.");

            return Ok(roomType);
        }

        // POST: api/RoomType
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RoomType model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.RoomTypes.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { roomTypeId = model.RoomTypeId }, model);
        }

        // PUT: api/RoomType/5
        [HttpPut("{roomTypeId}")]
        public async Task<IActionResult> Update(int roomTypeId, [FromBody] RoomType model)
        {
            if (roomTypeId != model.RoomTypeId)
                return BadRequest("ID mismatch.");

            var existing = await _context.RoomTypes.FindAsync(roomTypeId);
            if (existing == null)
                return NotFound($"RoomType with ID = {roomTypeId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/RoomType/5
        [HttpDelete("{roomTypeId}")]
        public async Task<IActionResult> Delete(int roomTypeId)
        {
            var roomType = await _context.RoomTypes.FindAsync(roomTypeId);
            if (roomType == null)
                return NotFound($"RoomType with ID = {roomTypeId} not found.");

            _context.RoomTypes.Remove(roomType);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
