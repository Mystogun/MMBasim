using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyManagement.API.Data;
using MoneyManagement.API.Entities;

namespace MoneyManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly DataContext _context;
        public TransactionsController(DataContext context)
        {
            _context = context;
        }

        // GET : api/transactions
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _context.Transactions.ToListAsync();
            return Ok(new { Transactions = transactions });
        }

        // GET : api/transactions/5
        [HttpGet("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById([FromRoute(Name = "id")] int transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            return Ok(new { Transaction = transaction });
        }

        // POST : api/transactions
        [HttpPost]
        public async Task<IActionResult> InsertTransaction([FromBody] Transaction transaction)
        {
            var result = await _context.Transactions.AddAsync(transaction);
            var changes = await _context.SaveChangesAsync();

            if (changes > 0)
            {
                return CreatedAtAction("GetById", new { Id = result.Entity.Id }, result.Entity);
            }

            return StatusCode(500);

        }

        // PUT : api/transactions/5
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] Transaction transaction)
        {
            _context.Transactions.Update(transaction);
            var changes = await _context.SaveChangesAsync();
            if (changes > 0)
            {
                return NoContent();
            }

            return StatusCode(500);
        }

        // DEL : api/transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute(Name = "id")] int transactionId)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(t => t.Id == transactionId);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            var changes = await _context.SaveChangesAsync();

            if (changes > 0)
            {
                return NoContent();
            }

            return StatusCode(500);
        }
    }
}