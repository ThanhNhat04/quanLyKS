using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public PaymentController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Payment
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var payments = await _context.Payments.ToListAsync();
            return Ok(payments);
        }

        // GET: api/Payment/5
        [HttpGet("{paymentId}")]
        public async Task<IActionResult> GetById(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
                return NotFound($"Payment with ID = {paymentId} not found.");

            return Ok(payment);
        }

        // POST: api/Payment
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Payment model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Payments.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { paymentId = model.PaymentId }, model);
        }

        // PUT: api/Payment/5
        [HttpPut("{paymentId}")]
        public async Task<IActionResult> Update(int paymentId, [FromBody] Payment model)
        {
            if (paymentId != model.PaymentId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Payments.FindAsync(paymentId);
            if (existing == null)
                return NotFound($"Payment with ID = {paymentId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Payment/5
        [HttpDelete("{paymentId}")]
        public async Task<IActionResult> Delete(int paymentId)
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null)
                return NotFound($"Payment with ID = {paymentId} not found.");

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
