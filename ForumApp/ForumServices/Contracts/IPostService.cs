namespace ForumServices.Contracts
{
    using ForumViewModels.Post;

    public interface IPostService
    {
        Task<IEnumerable<PostViewModel>> ListAllAsync();

        Task AddModelAsync(PostFormModel model);

        Task<PostFormModel> GetByIdAsync(string id);

        Task Edit(string id, PostFormModel model);

        Task DeleteByIdAsync(string id);
    }
}
