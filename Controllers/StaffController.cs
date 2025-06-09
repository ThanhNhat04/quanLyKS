using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public StaffController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Staff
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var staffList = await _context.Staffs.ToListAsync();
            return Ok(staffList);
        }

        // GET: api/Staff/5
        [HttpGet("{staffId}")]
        public async Task<IActionResult> GetById(int staffId)
        {
            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff == null)
                return NotFound($"Staff with ID = {staffId} not found.");

            return Ok(staff);
        }

        // POST: api/Staff
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Staff model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Staffs.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { staffId = model.StaffId }, model);
        }

        // PUT: api/Staff/5
        [HttpPut("{staffId}")]
        public async Task<IActionResult> Update(int staffId, [FromBody] Staff model)
        {
            if (staffId != model.StaffId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Staffs.FindAsync(staffId);
            if (existing == null)
                return NotFound($"Staff with ID = {staffId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Staff/5
        [HttpDelete("{staffId}")]
        public async Task<IActionResult> Delete(int staffId)
        {
            var staff = await _context.Staffs.FindAsync(staffId);
            if (staff == null)
                return NotFound($"Staff with ID = {staffId} not found.");

            _context.Staffs.Remove(staff);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
