namespace SoftUniBazar.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniBazar.Models;
    using System.Diagnostics;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (this.User?.Identity?.IsAuthenticated ?? false)
            {
                RedirectToAction("All", "Ad");
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