namespace Homies.Controllers
{
    using Homies.Services.Interfaces;
    using Homies.ViewModels.Event;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Security.Claims;
    using static Common.GeneralApplicationMessages;

    [Authorize]
    public class EventController : Controller
    {
        private readonly IEventTypeService eventTypeService;
        private readonly IEventService eventService;

        public EventController(IEventTypeService eventTypeService, IEventService eventService)
        {
            this.eventTypeService = eventTypeService;
            this.eventService = eventService;

        }

        [HttpGet]
        public async Task<IActionResult> Add()
        {
            EventPostModel model = new EventPostModel();

            try
            {
                model.Types = await this.eventTypeService.GetAllTypesAsync();
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(EventPostModel model)
        {
            DateTime start;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                this.ModelState.AddModelError(nameof(model.Start), DateTimeFormatMessage);
            }

            DateTime end;

            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                this.ModelState.AddModelError(nameof(model.End), DateTimeFormatMessage);
            }

            if (!this.ModelState.IsValid)
            {

                try
                {
                    model.Types = await this.eventTypeService.GetAllTypesAsync();
                }
                catch (Exception)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.View(model);
            }

            if (end < start)
            {
                this.ModelState.AddModelError(nameof(model.Start), StartDateMustBeBetweenEndDay);

                try
                {
                    model.Types = await this.eventTypeService.GetAllTypesAsync();
                }
                catch (Exception)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.View(model);
            }

            bool isTypeExisting;

            try
            {
                isTypeExisting = await this.eventTypeService.IsTypeExistingByIdAsync(model.TypeId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isTypeExisting)
            {
                this.ModelState.AddModelError(nameof(model.TypeId), TypeNotExistingMessage);

                try
                {
                    model.Types = await this.eventTypeService.GetAllTypesAsync();
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
                await this.eventService.AddEventAsync(model, start, end, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> All()
        {
            IEnumerable<EventAllViewModel> events;

            try
            {
                events = await this.eventService.GetAllEventsAsync();
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(events);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            bool isEventExisting;

            try
            {
                isEventExisting = await this.eventService.IsEventExistingByIdAsync(id);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isEventExisting)
            {
                return this.RedirectToAction("All", "Event");
            }

            EventDetailsViewModel model;

            try
            {
                model = await this.eventService.GetEventDetailsAsync(id);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            bool isEventExisting;
            bool isUserOwnerOfEvent;

            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                isEventExisting = await this.eventService.IsEventExistingByIdAsync(id);
                isUserOwnerOfEvent = await this.eventService.IsUserOwnerOfEventAsync(id, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isEventExisting || !isUserOwnerOfEvent)
            {
                return this.RedirectToAction("Error", "Home");
            }

            EventPostModel model;

            try
            {
                model = await this.eventService.GetEventForEditAsync(id);
                model.Types = await this.eventTypeService.GetAllTypesAsync();
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EventPostModel model)
        {
            bool isEventExisting;
            bool isUserOwnerOfEvent;

            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                isEventExisting = await this.eventService.IsEventExistingByIdAsync(id);
                isUserOwnerOfEvent = await this.eventService.IsUserOwnerOfEventAsync(id, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isEventExisting || !isUserOwnerOfEvent)
            {
                return this.RedirectToAction("Error", "Home");
            }

            DateTime start;

            if (!DateTime.TryParseExact(model.Start, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out start))
            {
                this.ModelState.AddModelError(nameof(model.Start), DateTimeFormatMessage);
            }

            DateTime end;

            if (!DateTime.TryParseExact(model.End, DateFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out end))
            {
                this.ModelState.AddModelError(nameof(model.End), DateTimeFormatMessage);
            }

            if (!this.ModelState.IsValid)
            {

                try
                {
                    model.Types = await this.eventTypeService.GetAllTypesAsync();
                }
                catch (Exception)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.View(model);
            }

            if (end < start)
            {
                this.ModelState.AddModelError(nameof(model.Start), StartDateMustBeBetweenEndDay);

                try
                {
                    model.Types = await this.eventTypeService.GetAllTypesAsync();
                }
                catch (Exception)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.View(model);
            }

            bool isTypeExisting;

            try
            {
                isTypeExisting = await this.eventTypeService.IsTypeExistingByIdAsync(model.TypeId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isTypeExisting)
            {
                this.ModelState.AddModelError(nameof(model.TypeId), TypeNotExistingMessage);

                try
                {
                    model.Types = await this.eventTypeService.GetAllTypesAsync();
                }
                catch (Exception)
                {
                    return this.RedirectToAction("Error", "Home");
                }

                return this.View(model);
            }

            try
            {
                await this.eventService.EditAsync(model, id, start, end);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Event");
        }

        [HttpPost]
        public async Task<IActionResult> Join(int id)
        {
            bool isEventExisting;
            bool isAlreadyJoined;
            bool isUserOwner;
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                isEventExisting = await this.eventService.IsEventExistingByIdAsync(id);
                isAlreadyJoined = await this.eventService.IsEventJoinedAsync(id, userId);
                isUserOwner = await this.eventService.IsUserOwnerOfEventAsync(id, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isEventExisting || isUserOwner)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (isAlreadyJoined)
            {
                return this.RedirectToAction("Joined", "Event");
            }

            try
            {
                await this.eventService.JoinEventAsync(id, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("Joined", "Event");
        }

        [HttpGet]
        public async Task<IActionResult> Joined()
        {
            IEnumerable<EventAllViewModel> events;

            try
            {
                string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
                events = await this.eventService.GetJoinedEvents(userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.View(events);
        }

        [HttpPost]
        public async Task<IActionResult> Leave(int id)
        {
            bool isEventExisting;
            bool isAlreadyJoined;
            bool isUserOwner;
            string userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                isEventExisting = await this.eventService.IsEventExistingByIdAsync(id);
                isAlreadyJoined = await this.eventService.IsEventJoinedAsync(id, userId);
                isUserOwner = await this.eventService.IsUserOwnerOfEventAsync(id, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isEventExisting || isUserOwner)
            {
                return this.RedirectToAction("Error", "Home");
            }

            if (!isAlreadyJoined)
            {
                return this.RedirectToAction("Joined", "Event");
            }

            try
            {
                await this.eventService.LeaveEventAsync(id, userId);
            }
            catch (Exception)
            {
                return this.RedirectToAction("Error", "Home");
            }

            return this.RedirectToAction("All", "Event");
        }
    }
}
