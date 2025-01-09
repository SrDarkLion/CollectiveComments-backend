using CollectiveComments.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectiveComments
{
    public class AppDbCotext : DbContext {
        private DbSet<Company> companies { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=postgres;username=postgres;password=money");
            base.OnConfiguring(optionsBuilder);
        }
    }
}