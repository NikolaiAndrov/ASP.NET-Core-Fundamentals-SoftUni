namespace ForumApp.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class PostController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
