namespace ForumData.Configuration
{
    using ForumData.Seeding;
    using ForumDataModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class PostEntityConfiguration : IEntityTypeConfiguration<Post>
    {
        private readonly PostSeeder seeder;

        public PostEntityConfiguration()
        {
            this.seeder = new PostSeeder();
        }

        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasData(this.seeder.CreatePosts());
        }
    }
}
