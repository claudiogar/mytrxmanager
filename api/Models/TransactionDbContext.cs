using Microsoft.EntityFrameworkCore;
using api.Models.DbModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<TransactionDbModel> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
        {
        }

        internal async Task<IEnumerable<TransactionDbModel>> GetTransactionsAsync(int? beforeId, int pageSize)
        {
            return await Transactions.Where(e => !beforeId.HasValue || e.Id < beforeId).OrderByDescending(trx => trx.Id).Take(pageSize).ToListAsync();
        }
    }
}
