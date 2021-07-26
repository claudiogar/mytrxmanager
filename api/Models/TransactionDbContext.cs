using Microsoft.EntityFrameworkCore;
using api.Models.DbModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public interface ITransactionDbContext
    {
        Task<IEnumerable<TransactionDbModel>> GetTransactionsBeforeIdAsync(int? beforeId, int pageSize);
        Task<IEnumerable<TransactionDbModel>> GetTransactionsAfterIdAsync(int? afterId, int pageSize);
        Task<TransactionDbModel> FindAsync(int id);
        Task AddAsync(TransactionDbModel trx);
        Task UpdateAsync(TransactionDbModel trx);
        Task RemoveAsync(TransactionDbModel trx);
    }

    public class TransactionDbContext : DbContext, ITransactionDbContext
    {
        public DbSet<TransactionDbModel> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
        {
        }

        public async Task<IEnumerable<TransactionDbModel>> GetTransactionsBeforeIdAsync(int? beforeId, int pageSize)
        {
            return await Transactions.Where(e => !beforeId.HasValue || e.Id < beforeId).OrderByDescending(trx => trx.Id).Take(pageSize).ToListAsync();
        }

        public async Task<IEnumerable<TransactionDbModel>> GetTransactionsAfterIdAsync(int? afterId, int pageSize)
        {
            return await Transactions.Where(e => !afterId.HasValue || e.Id > afterId).OrderBy(trx => trx.Id).Take(pageSize).OrderByDescending(trx => trx.Id).ToListAsync();
        }

        public async Task<TransactionDbModel> FindAsync(int id)
        {
            return await Transactions.FindAsync(id);
        }

        public async Task AddAsync(TransactionDbModel trx)
        {
            await Transactions.AddAsync(trx);

            await SaveChangesAsync();
        }

        public async Task RemoveAsync(TransactionDbModel trx)
        {
            Transactions.Remove(trx);

            await SaveChangesAsync();
        }

        public async Task UpdateAsync(TransactionDbModel trx)
        {
            Entry(trx).State = EntityState.Modified;

            await SaveChangesAsync();
        }
    }
}
