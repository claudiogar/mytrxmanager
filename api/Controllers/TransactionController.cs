using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api.Models;
using api.Models.ApiModels;
using System;
using api.Models.DbModels;
using Microsoft.Extensions.Logging;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionDbContext _context;
        private readonly ILogger _logger;

        public TransactionController(TransactionDbContext context, ILogger<TransactionController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Transaction?beforeId=1&pageSize=10
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionApiModel>>> GetTransactions([FromQuery] GetQueryParameters queryParameters)
        {
            _logger.LogTrace("{nr_transactions} have been requested.");
            return (await _context.GetTransactionsAsync(queryParameters.BeforeId, queryParameters.Limit)).Select(trx => trx.ToApiModel()).ToList();
        }

        // GET: api/Transaction/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionApiModel>> GetTransaction(int id)
        {
            if (id < 0)
            {
                ModelState.AddModelError(nameof(id), "id should be a positive value!");
                _logger.LogWarning("A transaction request for a TRX with negative id has been made.");

                return BadRequest(ModelState);
            }


            var transaction = await _context.Transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            _logger.LogTrace("TRX {trx_id} has been requested.", id);
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
                _logger.LogWarning("An attempt to alter a TRX via mismatching IDs has been made.");

                return BadRequest(ModelState);
            }

            TransactionDbModel trx = transaction.ToDbModel();

            try
            {
                _context.Entry(trx).State = EntityState.Modified;
                _logger.LogInformation("TRX with id {trx_id} has been modified.", id);

                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                _logger.LogWarning("TRX {trx_id} could not be altered due to a DB concurrency issue.");

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

                _logger.LogInformation("TRX with id {trx_id} has been created.", trx.Id);

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
                _logger.LogWarning("An attempt to delete TRX {trx_id}, which does not exist, has been made.",id);

                return NotFound();
            }

            _context.Transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            _logger.LogInformation("TRX {trx_id} has been deleted.", id);

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.Transactions.Any(e => e.Id == id);
        }
    }
}