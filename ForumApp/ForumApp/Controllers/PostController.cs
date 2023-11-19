namespace ForumApp.Controllers
{
    using ForumServices.Contracts;
    using ForumViewModels.Post;
    using Microsoft.AspNetCore.Mvc;

    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        public async Task<IActionResult> All()
        {
            IEnumerable<PostViewModel> posts = await this.postService.ListAllAsync();

            return View(posts);
        }
    }
}
