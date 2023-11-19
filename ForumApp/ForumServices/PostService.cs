namespace ForumServices
{
    using ForumData;
    using ForumServices.Contracts;
    using ForumViewModels.Post;

    public class PostService : IPostService
    {
        private readonly ForumDbContext forumDbContext;

        public PostService(ForumDbContext forumDbContext)
        {
            this.forumDbContext = forumDbContext;
        }

        public Task<IEnumerable<PostViewModel>> ListAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}
