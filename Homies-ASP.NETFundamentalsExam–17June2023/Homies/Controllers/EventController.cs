namespace Homies.Controllers
{
	using Homies.Services.Interfaces;
	using Homies.ViewModels;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.AspNetCore.Mvc;
	using System.Security.Claims;

	[Authorize]
	public class EventController : Controller
	{
		private readonly IEventService eventService;

        public EventController(IEventService eventService)
        {
            this.eventService = eventService;
        }
        public async Task<IActionResult> All()
		{
			ICollection<EventAllViewModel> events = await eventService.GetAllEventsAsync();
			return View(events);
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

		public async Task<IActionResult> Details(int Id)
		{
			EventDetailsViewModel eventDetails;

			try
			{
				eventDetails = await eventService.GetEventDetailsAsync(Id);
			}
			catch (Exception)
			{
				return RedirectToAction("All", "Event");
			}

			return View(eventDetails);
		}

		public async Task<IActionResult> Edit(int Id)
		{
			EventPostModel eventPost;

			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				eventPost = await eventService.GetEventForEditAsync(Id, userId);
				eventPost.Types = await eventService.GetAllTypesForEventAsync();
			}
			catch (Exception)
			{
				return RedirectToAction("All", "Event");
			}

			return View(eventPost);
		}

		[HttpPost]
		public async Task<IActionResult> Edit(int Id, EventPostModel eventPost)
		{
			if (!ModelState.IsValid)
			{
				eventPost.Types = await eventService.GetAllTypesForEventAsync();
				return View(eventPost);
			}

			try
			{
				string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
				await eventService.EditEventAsync(Id, eventPost, userId);
			}
			catch (Exception)
			{
			}

			return RedirectToAction("All", "Event");
		}
	}
}
