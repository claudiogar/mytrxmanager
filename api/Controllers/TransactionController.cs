using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Models.ApiModels;
using System;
using api.Models.DbModels;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionDbContext _context;

        public TransactionController(TransactionDbContext context)
        {
            _context = context;
        }

        // GET: api/Transaction?beforeId=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionApiModel>>> GetTransactions([FromQuery] GetQueryParameters queryParameters)
        {
            return (await _context.GetTransactionsAsync(queryParameters.BeforeId, queryParameters.Limit)).Select(trx => trx.ToApiModel()).ToList();
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionApiModel>> GetTransaction(int id)
        {
            if (id < 0)
            {
                ModelState.AddModelError(nameof(id), "id should be a positive value!");
                return BadRequest(ModelState);
            }


            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction.ToApiModel();
        }

        // PUT: api/Transaction/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, TransactionApiModel transaction)
        {
            if (id != transaction.Id)
            {
                ModelState.AddModelError(nameof(id), "id parameter and model id do not match!");

                return BadRequest(ModelState);
            }

            TransactionDbModel trx;
            try
            {
                trx = transaction.ToDbModel();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }

            try
            {
                _context.Entry(trx).State = EntityState.Modified;
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transaction
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TransactionApiModel>> PostTransaction(TransactionApiModel transaction)
        {
            try
            {
                TransactionDbModel trx = transaction.ToDbModel();

                _context.Transactions.Add(trx);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransaction", new { id = trx.Id }, trx.ToApiModel());
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Transaction/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            TransactionDbModel transaction = await _context.Transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}