namespace SoftUniBazar.Controllers
{
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using SoftUniBazar.Services.Interfaces;
	using SoftUniBazar.ViewModels;

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
	}
}
