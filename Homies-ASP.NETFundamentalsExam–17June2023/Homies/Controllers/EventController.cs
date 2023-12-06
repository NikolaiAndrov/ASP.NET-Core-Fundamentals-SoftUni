namespace Homies.Controllers
{
	using Homies.Services.Interfaces;
	using Homies.ViewModels;
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Claims;

	public class EventController : Controller
	{
		private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public IActionResult All()
		{
			return View();
		}

		public async Task<IActionResult> Add()
		{
			EventPostModel model = new EventPostModel
			{
				Types = await eventService.GetAllTypesForEventAsync()
			};

			return View(model);
		}

		[HttpPost]
		public async Task<IActionResult> Add(EventPostModel model)
		{
			if (!ModelState.IsValid)
			{
				model.Types = await eventService.GetAllTypesForEventAsync();
				return View(model);
			}


			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				await eventService.AddEventAsync(model, userId);
			}
			catch (Exception)
			{
				model.Types = await eventService.GetAllTypesForEventAsync();
				return View(model);
			}

			return RedirectToAction("All", "Event");
		}
	}
}
