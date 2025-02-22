using CollectiveComments.Models;
using Microsoft.EntityFrameworkCore;

namespace CollectiveComments
{
    public class AppDbContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("host=localhost;port=5432;database=co;username=postgres;password=money");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Company>()
                .HasKey(c => c.Id);

            modelBuilder.Entity<Company>()
                .HasAlternateKey(c => c.Code);

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Code)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .HasIndex(c => c.Name)
                .IsUnique();

            modelBuilder.Entity<Company>()
                .Property(c => c.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Company>()
                .Property(c => c.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Feedback>()
                .HasKey(f => f.Id);

            modelBuilder.Entity<Feedback>()
                .Property(f => f.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            modelBuilder.Entity<Feedback>()
                .Property(f => f.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP");

            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.Company)
                .WithMany(f => f.Feedbacks)
                .HasForeignKey(f => f.CompanyCode)
                .HasPrincipalKey(c => c.Code);


            base.OnModelCreating(modelBuilder);
        }
    }
}