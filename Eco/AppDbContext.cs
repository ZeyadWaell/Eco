using Microsoft.EntityFrameworkCore;

namespace Eco
{
    public class AppDbContext : DbContext
    {
        // Define DbSet properties for your entities
        public DbSet<CarDetails> Cars { get; set; }

        // Constructor accepting DbContextOptions
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure your models here
            modelBuilder.Entity<CarDetails>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Lang).HasMaxLength(100).IsRequired();
                entity.Property(c => c.Tud).HasMaxLength(100).IsRequired();
            });
        }
    }
}
