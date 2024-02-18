namespace SeminarHub.Data
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

    using SeminarHub.Data.Models;

    public class SeminarHubDbContext : IdentityDbContext
    {
        public SeminarHubDbContext(DbContextOptions<SeminarHubDbContext> options)
            : base(options)
        {
        }

        public DbSet<Seminar> Seminars { get; set; } = null!;

        public DbSet<SeminarParticipant> SeminarsPartecipiants { get; set; } = null!;

        public DbSet<Category> Categories { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<SeminarParticipant>(entity =>
            {
                entity.HasKey(k => new { k.SeminarId, k.ParticipantId });

                entity.HasOne(sp => sp.Seminar)
                    .WithMany(sp => sp.SeminarsParticipants)
                    .OnDelete(DeleteBehavior.NoAction);

                entity.HasOne(sp => sp.Participant)
                    .WithMany()
                    .OnDelete(DeleteBehavior.NoAction);
            });

            builder
               .Entity<Category>()
               .HasData(new Category()
               {
                   Id = 1,
                   Name = "Technology & Innovation"
               },
               new Category()
               {
                   Id = 2,
                   Name = "Business & Entrepreneurship"
               },
               new Category()
               {
                   Id = 3,
                   Name = "Science & Research"
               },
               new Category()
               {
                   Id = 4,
                   Name = "Arts & Culture"
               });

            base.OnModelCreating(builder);
        }
    }
}