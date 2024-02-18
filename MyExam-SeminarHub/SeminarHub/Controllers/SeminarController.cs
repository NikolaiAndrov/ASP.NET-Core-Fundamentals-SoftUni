namespace SeminarHub.Controllers
{
	using System.Security.Claims;
	using System.Globalization;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;

	using Models.Seminar;
	using Services.Interfaces;
	using static Common.GeneralApplicationConstants;
	using static Common.ApplicationErrorMessages;

	[Authorize]
	public class SeminarController : Controller
	{
		private readonly ICategoryService categoryService;
		private readonly ISeminarService seminarService;

        public SeminarController(ICategoryService categoryService, ISeminarService seminarService)
        {
            this.categoryService = categoryService;
			this.seminarService = seminarService;
        }

		[HttpGet]
        public async Task<IActionResult> Add()
		{
			SeminarPostModel model = new SeminarPostModel();

			try
			{
				model.Categories = await this.categoryService.GetAllCategoriesAsync();
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(SeminarPostModel model)
		{
			DateTime dateAndTime;

			if (!DateTime.TryParseExact(model.DateAndTime, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
			{
				this.ModelState.AddModelError(nameof(model.DateAndTime), DateTimeErrorMessage);
			}

			bool isCategoryExisting;

			try
			{
				isCategoryExisting = await this.categoryService.IsCategoryExistingByIdAsync(model.CategoryId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isCategoryExisting)
			{
				this.ModelState.AddModelError(nameof(model.CategoryId), CategoryNotExistingMessage);
			}

			if (!this.ModelState.IsValid)
			{
				try
				{
					model.Categories = await this.categoryService.GetAllCategoriesAsync();
				}
				catch (Exception)
				{
					return this.RedirectToAction("All", "Seminar");
				}

				return this.View(model);
			}

			try
			{
				string userId = this.GetUserId();
				await this.seminarService.AddSeminarAsync(model, userId, dateAndTime);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.RedirectToAction("All", "Seminar");
		}

		[HttpGet]
		public async Task<IActionResult> All()
		{
			IEnumerable<SeminarAllViewModel> seminars;

			try
			{
				seminars = await this.seminarService.GetAllSeminarAsync();
			}
			catch (Exception)
			{
				return this.RedirectToAction("Error", "Home");
			}

			return this.View(seminars);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int id)
		{
			bool isSeminarExisting;
			bool isUserOwner;

			string userId = this.GetUserId();

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(id);
				isUserOwner = await this.seminarService.IsUserOwnerOfSeminarAsync(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting || !isUserOwner)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			SeminarPostModel model;

			try
			{
				model = await this.seminarService.GetSeminarForEditAsync(id);
				model.Categories = await this.categoryService.GetAllCategoriesAsync();
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int id, SeminarPostModel model)
		{
			bool isSeminarExisting;
			bool isUserOwner;

			string userId = this.GetUserId();

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(id);
				isUserOwner = await this.seminarService.IsUserOwnerOfSeminarAsync(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting || !isUserOwner)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			DateTime dateAndTime;

			if (!DateTime.TryParseExact(model.DateAndTime, DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateAndTime))
			{
				this.ModelState.AddModelError(nameof(model.DateAndTime), DateTimeErrorMessage);
			}

			bool isCategoryExisting;

			try
			{
				isCategoryExisting = await this.categoryService.IsCategoryExistingByIdAsync(model.CategoryId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isCategoryExisting)
			{
				this.ModelState.AddModelError(nameof(model.CategoryId), CategoryNotExistingMessage);
			}

			if (!this.ModelState.IsValid)
			{
				try
				{
					model.Categories = await this.categoryService.GetAllCategoriesAsync();
				}
				catch (Exception)
				{
					return this.RedirectToAction("All", "Seminar");
				}

				return this.View(model);
			}

			try
			{
				await this.seminarService.EditSeminarAsync(id, model, dateAndTime);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.RedirectToAction("All", "Seminar");
		}

		[HttpGet]
		public async Task<IActionResult> Details(int id)
		{
			bool isSeminarExisting;

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(id);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			SeminarDetailsViewModel model;

			try
			{
				model = await this.seminarService.GetSeminarDetailsAsync(id);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(int id)
		{
			bool isSeminarExisting;
			bool isUserOwner;

			string userId = this.GetUserId();

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(id);
				isUserOwner = await this.seminarService.IsUserOwnerOfSeminarAsync(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting || !isUserOwner)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			SeminarDeleteViewModel model;

			try
			{
				model = await this.seminarService.GetSeminarForDeleteAsync(id);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.View(model);
		}

		[HttpPost]
		public async Task<IActionResult> DeleteConfirmed(SeminarDeleteViewModel model)
		{
			bool isSeminarExisting;
			bool isUserOwner;

			string userId = this.GetUserId();

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(model.Id);
				isUserOwner = await this.seminarService.IsUserOwnerOfSeminarAsync(model.Id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting || !isUserOwner)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			try
			{
				await this.seminarService.DeleteSeminarAsync(model.Id);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.RedirectToAction("All", "Seminar");
		}

		[HttpGet]
		public async Task<IActionResult> Joined()
		{
			IEnumerable<SeminarJoinedViewModel> seminars;

			try
			{
				string userId = this.GetUserId();
				seminars = await this.seminarService.GetJoinedSeminarsAsync(userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.View(seminars);
		}

		[HttpPost]
		public async Task<IActionResult> Join(int id)
		{
			bool isSeminarExisting;
			bool isUserOwner;
			bool isInCollection;
			
			string userId = this.GetUserId();

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(id);
				isUserOwner = await this.seminarService.IsUserOwnerOfSeminarAsync(id, userId);
				isInCollection = await this.seminarService.IsAlreadyInCollectionAsync(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting || isUserOwner || isInCollection)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			try
			{
				await this.seminarService.AddSeminarToCollection(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.RedirectToAction("Joined", "Seminar");
		}

		[HttpPost]
		public async Task<IActionResult> Leave(int id)
		{
			bool isSeminarExisting;
			bool isUserOwner;
			bool isInCollection;

			string userId = this.GetUserId();

			try
			{
				isSeminarExisting = await this.seminarService.IsSeminarExistingByIdAsync(id);
				isUserOwner = await this.seminarService.IsUserOwnerOfSeminarAsync(id, userId);
				isInCollection = await this.seminarService.IsAlreadyInCollectionAsync(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			if (!isSeminarExisting || isUserOwner || !isInCollection)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			try
			{
				await this.seminarService.RemoveSeminarFromCollection(id, userId);
			}
			catch (Exception)
			{
				return this.RedirectToAction("All", "Seminar");
			}

			return this.RedirectToAction("Joined", "Seminar");
		}

		private string GetUserId()
			=> this.User.FindFirstValue(ClaimTypes.NameIdentifier);
	}
}
