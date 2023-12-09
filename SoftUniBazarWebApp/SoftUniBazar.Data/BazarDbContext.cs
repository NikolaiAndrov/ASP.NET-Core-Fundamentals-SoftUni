namespace SoftUniBazar.Data
{
	using Microsoft.AspNetCore.Identity;
	using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using SoftUniBazar.Models;

    public class BazarDbContext : IdentityDbContext
    {
        public BazarDbContext(DbContextOptions<BazarDbContext> options)
            : base(options)
        {
        }

        public DbSet<Ad> Ads { get; set; } = null!;

        public DbSet<AdBuyer> AdsBuyers { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AdBuyer>(entity =>
            {
                entity.HasKey(k => new { k.BuyerId, k.AdId });
            });

            modelBuilder.Entity<AdBuyer>(entity =>
            {
                entity.HasOne(ab => ab.Buyer)
                    .WithMany()
                    .HasForeignKey(ab => ab.BuyerId)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(a => a.Ad)
                    .WithMany()
                    .HasForeignKey(a  => a.AdId)
                    .OnDelete(DeleteBehavior.NoAction);
            });

            modelBuilder
                .Entity<Category>()
                .HasData(new Category()
                {
                    Id = 1,
                    Name = "Books"
                },
                new Category()
                {
                    Id = 2,
                    Name = "Cars"
                },
                new Category()
                {
                    Id = 3,
                    Name = "Clothes"
                },
                new Category()
                {
                    Id = 4,
                    Name = "Home"
                },
                new Category()
                {
                    Id = 5,
                    Name = "Technology"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}