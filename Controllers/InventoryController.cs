using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public InventoryController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Inventory
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _context.Inventories.ToListAsync();
            return Ok(items);
        }

        // GET: api/Inventory/5
        [HttpGet("{inventoryId}")]
        public async Task<IActionResult> GetById(int inventoryId)
        {
            var item = await _context.Inventories.FindAsync(inventoryId);
            if (item == null)
                return NotFound($"Inventory item with ID = {inventoryId} not found.");

            return Ok(item);
        }

        // POST: api/Inventory
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Inventory model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Inventories.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { inventoryId = model.InventoryId }, model);
        }

        // PUT: api/Inventory/5
        [HttpPut("{inventoryId}")]
        public async Task<IActionResult> Update(int inventoryId, [FromBody] Inventory model)
        {
            if (inventoryId != model.InventoryId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Inventories.FindAsync(inventoryId);
            if (existing == null)
                return NotFound($"Inventory item with ID = {inventoryId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Inventory/5
        [HttpDelete("{inventoryId}")]
        public async Task<IActionResult> Delete(int inventoryId)
        {
            var item = await _context.Inventories.FindAsync(inventoryId);
            if (item == null)
                return NotFound($"Inventory item with ID = {inventoryId} not found.");

            _context.Inventories.Remove(item);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
