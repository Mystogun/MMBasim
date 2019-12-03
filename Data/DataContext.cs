using Microsoft.EntityFrameworkCore;
using MoneyManagement.API.Entities;

namespace MoneyManagement.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Transaction> Transactions { get; set; }
    }
}