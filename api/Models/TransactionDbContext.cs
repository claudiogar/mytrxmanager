using Microsoft.EntityFrameworkCore;

namespace api.Models
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<Transaction> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
        {
        }
    }
}
