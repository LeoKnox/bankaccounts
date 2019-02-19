using Microsoft.EntityFrameworkCore;

namespace bankAccount.Models
{
    public class bankAccountContext: DbContext
    {
        public bankAccountContext(DbContextOptions<bankAccountContext> options):base(options) {}

        public DbSet<Users> MyUsers {get; set;}
        public DbSet<Transactions> MyTransactions {get; set;}
    }
}