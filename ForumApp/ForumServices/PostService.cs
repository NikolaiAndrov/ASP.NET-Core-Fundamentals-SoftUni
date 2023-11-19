namespace ForumServices
{
    using Microsoft.EntityFrameworkCore;

    using ForumData;
    using ForumServices.Contracts;
    using ForumViewModels.Post;
	using ForumDataModels;

	public class PostService : IPostService
    {
        private readonly ForumDbContext forumDbContext;

        public PostService(ForumDbContext forumDbContext)
        {
            this.forumDbContext = forumDbContext;
        }

        public async Task<IEnumerable<PostViewModel>> ListAllAsync()
        {
            IEnumerable<PostViewModel> posts = await forumDbContext.Posts
                .Select(p => new PostViewModel
                {
                    Id = p.Id.ToString(),
                    Title = p.Title,
                    Content = p.Content,
                })
                .ToArrayAsync();

            return posts;
        }

		public async Task AddModelAsync(PostFormModel model)
		{
			Post post = new Post
            {
                Title = model.Title,
                Content = model.Content,
            };

            await forumDbContext.Posts.AddAsync(post);
            await forumDbContext.SaveChangesAsync();
		}
	}
}
