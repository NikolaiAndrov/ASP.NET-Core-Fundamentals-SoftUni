namespace ForumServices.Contracts
{
    using ForumViewModels.Post;

    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> ListAllAsync();

        Task AddModelAsync(PostFormModel model);
    }
}
