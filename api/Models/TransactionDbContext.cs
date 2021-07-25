using Microsoft.EntityFrameworkCore;
using api.Models.DbModels;

namespace api.Models
{
    public class TransactionDbContext : DbContext
    {
        public DbSet<TransactionDbModel> Transactions { get; set; }

        public TransactionDbContext(DbContextOptions<TransactionDbContext> options):base(options)
        {
        }
    }
}
