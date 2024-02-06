using Homies.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Homies.Data
{
    public class HomiesDbContext : IdentityDbContext
    {
        public HomiesDbContext(DbContextOptions<HomiesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; } = null!;

        public DbSet<EventType> Types { get; set; } = null!;

        public DbSet<EventParticipant> EventsParticipants { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EventParticipant>()
                .HasKey(k => new { k.HelperId, k.EventId});

            modelBuilder.Entity<EventParticipant>()
                .HasOne(ep => ep.Event)
                .WithMany(ep => ep.EventsParticipants)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder
                .Entity<EventType>()
                .HasData(new EventType()
                {
                    Id = 1,
                    Name = "Animals"
                },
                new EventType()
                {
                    Id = 2,
                    Name = "Fun"
                },
                new EventType()
                {
                    Id = 3,
                    Name = "Discussion"
                },
                new EventType()
                {
                    Id = 4,
                    Name = "Work"
                });

            base.OnModelCreating(modelBuilder);
        }
    }
}