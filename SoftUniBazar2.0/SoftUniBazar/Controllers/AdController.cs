namespace SoftUniBazar.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using SoftUniBazar.Services.Interfaces;
    using SoftUniBazar.ViewModels.Ad;

    public class AdController : Controller
    {
        private readonly IAdService adService;
        private readonly ICategoryService categoryService;

        public AdController(IAdService adService, ICategoryService categoryService)
        {
            this.adService = adService;
            this.categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            AdPostModel model = new AdPostModel();

            try
            {
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }
    }
}
