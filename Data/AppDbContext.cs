using CollectiveComments.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectiveComments
{
    public class AppDbCotext : DbContext
    {
        public DbSet<Company> companies { get; set; }

        public DbSet<Feedback> feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=postgres;username=postgres;password=money");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()"); // PostgreSQL UUID generator

            modelBuilder.Entity<Company>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP"); // Default to current timestamp
            ////////////////////////////////////////////////////////////////////
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Feedback>()
                .Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()"); // PostgreSQL UUID generator

            // Configuração da tabela feedbacks
            modelBuilder.Entity<Feedback>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Company)
                .WithMany()
                .HasForeignKey(f => f.CompanyId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
