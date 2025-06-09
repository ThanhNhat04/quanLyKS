using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public InvoiceController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var invoices = await _context.Invoices.ToListAsync();
            return Ok(invoices);
        }

        // GET: api/Invoice/5
        [HttpGet("{invoiceId}")]
        public async Task<IActionResult> GetById(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
                return NotFound($"Invoice with ID = {invoiceId} not found.");

            return Ok(invoice);
        }

        // POST: api/Invoice
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Invoice model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Invoices.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { invoiceId = model.InvoiceId }, model);
        }

        // PUT: api/Invoice/5
        [HttpPut("{invoiceId}")]
        public async Task<IActionResult> Update(int invoiceId, [FromBody] Invoice model)
        {
            if (invoiceId != model.InvoiceId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Invoices.FindAsync(invoiceId);
            if (existing == null)
                return NotFound($"Invoice with ID = {invoiceId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Invoice/5
        [HttpDelete("{invoiceId}")]
        public async Task<IActionResult> Delete(int invoiceId)
        {
            var invoice = await _context.Invoices.FindAsync(invoiceId);
            if (invoice == null)
                return NotFound($"Invoice with ID = {invoiceId} not found.");

            _context.Invoices.Remove(invoice);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
