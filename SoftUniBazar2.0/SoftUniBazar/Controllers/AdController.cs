namespace SoftUniBazar.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniBazar.Services.Interfaces;
    using SoftUniBazar.ViewModels.Ad;
    using System.Security.Claims;
    using static Common.GeneralApplicationMessages;

    [Authorize]
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

        [HttpPost]
        public async Task<IActionResult> Add(AdPostModel model)
        {
            bool isCategoryExisting;

            try
            {
                isCategoryExisting = await this.categoryService.IsCategoryExistingByIdAsync(model.CategoryId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isCategoryExisting)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), CategoryNotExisting);
            }

            if (!this.ModelState.IsValid)
            {
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

            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                await this.adService.AddNewAdAsync(model, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Ad");
        }
    }
}
