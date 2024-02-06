namespace Homies.Controllers
{
    using Homies.ViewModels;
    using Microsoft.AspNetCore.Mvc;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                return this.RedirectToAction("All", "Event");
            }

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}