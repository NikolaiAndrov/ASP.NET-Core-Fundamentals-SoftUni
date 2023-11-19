namespace ForumApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using ForumServices.Contracts;
    using ForumViewModels.Post;
    using static ForumCommon.Validations.PostErrorMessages;

    public class PostController : Controller
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<PostViewModel> posts = await this.postService.ListAllAsync();

            return View(posts);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(PostFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.postService.AddModelAsync(model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", AddingErrorOccured);
                return View(model);
            }

            return RedirectToAction(nameof(All));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            try
            {
                PostFormModel postViewModel = await this.postService.GetByIdAsync(id);
                return View(postViewModel);
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(All));
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, PostFormModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                await this.postService.Edit(id, model);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", EditErrorOccured);
                return View(model);
            }

			return RedirectToAction(nameof(All));
		}
    }
}
