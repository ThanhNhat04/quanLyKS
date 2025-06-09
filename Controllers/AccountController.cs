using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelManagement.Models;
using System.Linq;

namespace QLKhachSan.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly HotelDbContext _context;

        public AccountController(HotelDbContext context)
        {
            _context = context;
        }

        // GET: api/Account
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Account>>> GetAll()
        {
            return await _context.Accounts
                                 .Include(a => a.Role)
                                 .ToListAsync();
        }

        // GET: api/Account/5
        [HttpGet("{accountId}")]
        public async Task<ActionResult<Account>> GetById(int accountId)
        {
            var account = await _context.Accounts
                                        .Include(a => a.Role)
                                        .FirstOrDefaultAsync(a => a.AccountId == accountId);
            if (account == null)
            {
                return NotFound();
            }

            return account;
        }

        // POST: api/Account
        [HttpPost]
        public async Task<ActionResult<Account>> Create(Account model)
        {
            // Kiểm tra Role tồn tại
            var role = await _context.Roles.FindAsync(model.RoleId);
            if (role == null)
            {
                return BadRequest("Invalid RoleId.");
            }

            _context.Accounts.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { accountId = model.AccountId }, model);
        }

        // PUT: api/Account/5
        [HttpPut("{accountId}")]
        public async Task<IActionResult> Update(int accountId, Account model)
        {
            if (accountId != model.AccountId)
            {
                return BadRequest("Account ID mismatch.");
            }

            // Kiểm tra Role tồn tại
            var role = await _context.Roles.FindAsync(model.RoleId);
            if (role == null)
            {
                return BadRequest("Invalid RoleId.");
            }

            _context.Entry(model).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AccountExists(accountId))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/Account/5
        [HttpDelete("{accountId}")]
        public async Task<IActionResult> Delete(int accountId)
        {
            var account = await _context.Accounts.FindAsync(accountId);
            if (account == null)
            {
                return NotFound();
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AccountExists(int id)
        {
            return _context.Accounts.Any(e => e.AccountId == id);
        }
    }
}
