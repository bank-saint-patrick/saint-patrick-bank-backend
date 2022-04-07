using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SPatrickBack.Model;

namespace SPatrickBack.Authentication
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProducType> ProductTypes { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }
        public DbSet<UserContact> UserContacts { get; set; }

        public DbSet<OperationLog> OperationsLogs { get; set; }
    }

}
