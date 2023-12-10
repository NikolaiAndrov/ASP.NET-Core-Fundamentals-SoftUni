namespace SoftUniBazar.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using SoftUniBazar.Services.Interfaces;
	using SoftUniBazar.ViewModels;
	using System.Security.Claims;

	[Authorize]
	public class AdController : Controller
	{
		private readonly IAdService adService;

        public AdController(IAdService adService)
        {
            this.adService = adService;
        }

        public async Task<IActionResult> All()
		{
			ICollection<ListAllAdViewModel> allAds = await adService.GetAllAdsAsync();

			return View(allAds);
		}

		public async Task<IActionResult> Add()
		{
			AdPostViewModel post = new AdPostViewModel
			{
				Categories = await adService.GetCategoriesAsync()
			};

			return View(post);
		}

		[HttpPost]
		public async Task<IActionResult> Add(AdPostViewModel post)
		{
			if (!ModelState.IsValid)
			{
				post.Categories = await adService.GetCategoriesAsync();
				return View(post);
			}

			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				await adService.AddingNewAdAsync(post, userId);
			}
			catch (Exception)
			{
			}

			return RedirectToAction("All", "Ad");
		}

		public async Task<IActionResult> Edit(int Id)
		{
			AdPostViewModel model;

			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				model = await adService.GetModelForEditAsync(Id, userId);
			}
			catch (Exception)
			{
				return RedirectToAction("All", "Ad");
			}

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(AdPostViewModel model, int Id)
		{
			if (!ModelState.IsValid)
			{
				model.Categories = await adService.GetCategoriesAsync();
				return View(model);
			}

			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				await adService.EditPostAsync(Id, model, userId);
			}
			catch (Exception)
			{
			}

			return RedirectToAction("All", "Ad");
		}

		public async Task<IActionResult> Cart()
		{
			string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty;

			ICollection<ListAllAdViewModel> cartAds = await adService.GetCartAsync(userId);

			return View(cartAds);
		}
	}
}
