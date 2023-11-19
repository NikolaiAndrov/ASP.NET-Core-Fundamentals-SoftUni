namespace ForumData
{
    using ForumData.Configuration;
    using ForumDataModels;
    using Microsoft.EntityFrameworkCore;

    public class ForumDbContext : DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
