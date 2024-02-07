namespace SoftUniBazar.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using SoftUniBazar.Services.Interfaces;
    using SoftUniBazar.ViewModels.Ad;
    using System.Security.Claims;
    using static Common.GeneralApplicationConstants;

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

            if (model.Price < 0m)
            {
                this.ModelState.AddModelError(nameof(model.Price), NegativePriceError);
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

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<AdAllViewModel> ads;

            try
            {
                ads = await this.adService.GetAllAdsAsync();
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(ads);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool isAdExisting;
            bool isUserOwnerOfAd;

            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                isAdExisting = await this.adService.IsAdExistingByIdAsync(id);
                isUserOwnerOfAd = await this.adService.IsUserOwnerOfAdAsync(userId, id);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isAdExisting || !isUserOwnerOfAd)
            {
                return this.RedirectToAction("Error", "Home");
            }

            AdPostModel model;

            try
            {
                model = await this.adService.GetAdForEditAsync(id);
                model.Categories = await this.categoryService.GetAllCategoriesAsync();
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, AdPostModel model)
        {
            bool isAdExisting;
            bool isUserOwnerOfAd;
            bool isCategoryExisting;

            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                isAdExisting = await this.adService.IsAdExistingByIdAsync(id);
                isUserOwnerOfAd = await this.adService.IsUserOwnerOfAdAsync(userId, id);
                isCategoryExisting = await this.categoryService.IsCategoryExistingByIdAsync(model.CategoryId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isAdExisting || !isUserOwnerOfAd)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isCategoryExisting)
            {
                this.ModelState.AddModelError(nameof(model.CategoryId), CategoryNotExisting);
            }

            if (model.Price < 0m)
            {
                this.ModelState.AddModelError(nameof(model.Price), NegativePriceError);
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
                await this.adService.EditAdAsync(model, id);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Ad");
        }
    }
}
