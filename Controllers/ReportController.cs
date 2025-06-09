using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using HotelManagement.Models;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public ReportController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Report
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _context.Reports.ToListAsync();
            return Ok(reports);
        }

        // GET: api/Report/5
        [HttpGet("{reportId}")]
        public async Task<IActionResult> GetById(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                return NotFound($"Report with ID = {reportId} not found.");

            return Ok(report);
        }

        // POST: api/Report
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Report model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Reports.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { reportId = model.ReportId }, model);
        }

        // PUT: api/Report/5
        [HttpPut("{reportId}")]
        public async Task<IActionResult> Update(int reportId, [FromBody] Report model)
        {
            if (reportId != model.ReportId)
                return BadRequest("ID mismatch.");

            var existing = await _context.Reports.FindAsync(reportId);
            if (existing == null)
                return NotFound($"Report with ID = {reportId} not found.");

            _context.Entry(existing).CurrentValues.SetValues(model);
            await _context.SaveChangesAsync();

            return Ok("Update successful.");
        }

        // DELETE: api/Report/5
        [HttpDelete("{reportId}")]
        public async Task<IActionResult> Delete(int reportId)
        {
            var report = await _context.Reports.FindAsync(reportId);
            if (report == null)
                return NotFound($"Report with ID = {reportId} not found.");

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();

            return Ok("Deleted successfully.");
        }
    }
}
