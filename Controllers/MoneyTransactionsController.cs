using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SharpBankAPI.Models;

namespace SharpBankAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyTransactionsController : ControllerBase
    {
        private readonly SharpBankContext _context;

        public MoneyTransactionsController(SharpBankContext context)
        {
            _context = context;
        }

        // GET: api/MoneyTransactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoneyTransaction>>> GetMoneyTransactions()
        {
            return await _context.MoneyTransactions.ToListAsync();
        }

        // GET: api/MoneyTransactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoneyTransaction>> GetMoneyTransaction(int id)
        {
            var moneyTransaction = await _context.MoneyTransactions.FindAsync(id);

            if (moneyTransaction == null)
            {
                return NotFound();
            }

            return moneyTransaction;
        }

        // PUT: api/MoneyTransactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoneyTransaction(int id, MoneyTransaction moneyTransaction)
        {
            if (id != moneyTransaction.TransactionId)
            {
                return BadRequest();
            }

            _context.Entry(moneyTransaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoneyTransactionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MoneyTransactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoneyTransaction>> PostMoneyTransaction(MoneyTransaction moneyTransaction)
        {
            _context.MoneyTransactions.Add(moneyTransaction);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMoneyTransaction", new { id = moneyTransaction.TransactionId }, moneyTransaction);
        }

        // DELETE: api/MoneyTransactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoneyTransaction(int id)
        {
            var moneyTransaction = await _context.MoneyTransactions.FindAsync(id);
            if (moneyTransaction == null)
            {
                return NotFound();
            }

            _context.MoneyTransactions.Remove(moneyTransaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoneyTransactionExists(int id)
        {
            return _context.MoneyTransactions.Any(e => e.TransactionId == id);
        }
    }
}
