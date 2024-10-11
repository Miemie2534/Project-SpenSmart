using Microsoft.EntityFrameworkCore;

namespace Project_SpenSmart.Models.Data
{
    public class SpenSmartDBContext : DbContext
    {
        public DbSet<Expense> Expenses { get; set; }

        public SpenSmartDBContext(DbContextOptions<SpenSmartDBContext> options) : base(options) 
        {
            
        }
    }
}
