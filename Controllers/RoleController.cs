using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public RoleController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Role
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var roles = await _context.Roles.ToListAsync();
            return Ok(roles);
        }

        // GET: api/Role/5
        [HttpGet("{roleId}")]
        public async Task<IActionResult> GetById(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
                return NotFound($"Role with ID = {roleId} not found.");

            return Ok(role);
        }

        // POST: api/Role
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Role model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Roles.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { roleId = model.RoleId }, model);
        }

        // PUT: api/Role/5
        [HttpPut("{roleId}")]
        public async Task<IActionResult> Update(int roleId, [FromBody] Role model)
        {
            if (roleId != model.RoleId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Roles.FindAsync(roleId);
            if (existing == null)
                return NotFound($"Role with ID = {roleId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Role/5
        [HttpDelete("{roleId}")]
        public async Task<IActionResult> Delete(int roleId)
        {
            var role = await _context.Roles.FindAsync(roleId);
            if (role == null)
                return NotFound($"Role with ID = {roleId} not found.");

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
