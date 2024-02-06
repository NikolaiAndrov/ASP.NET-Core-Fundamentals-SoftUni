namespace Homies.Services
{
    using Homies.Data;
    using Homies.Data.Models;
    using Homies.Services.Interfaces;
    using Homies.ViewModels.Event;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Common.GeneralApplicationMessages;

    public class EventService : IEventService
    {
        private readonly HomiesDbContext dbContext;

        public EventService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddEventAsync(EventPostModel model, DateTime startDate, DateTime endDate, string userId)
        {
            Event newEvent = new Event
            {
                Name = model.Name,
                Description = model.Description,
                OrganiserId = userId,
                CreatedOn = DateTime.Now,
                Start = startDate,
                End = endDate,
                TypeId = model.TypeId,
            };

            await this.dbContext.Events.AddAsync(newEvent);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAsync(EventPostModel model, int eventId, DateTime startDate, DateTime endDate)
        {
            Event eventForEdit = await this.dbContext.Events
                .FirstAsync(e => e.Id == eventId);

            eventForEdit.Name = model.Name;
            eventForEdit.Description = model.Description;
            eventForEdit.Start = startDate;
            eventForEdit.End = endDate;
            eventForEdit.TypeId = model.TypeId;

            await this.dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<EventAllViewModel>> GetAllEventsAsync()
        {
            IEnumerable<EventAllViewModel> events = await this.dbContext.Events
                .Select(e => new EventAllViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Start = e.Start.ToString(DateFormat),
                    Type = e.Type.Name,
                    Organiser = e.Organiser.UserName
                })
                .ToArrayAsync();

            return events;
        }

        public async Task<EventDetailsViewModel> GetEventDetailsAsync(int eventId)
        {
            EventDetailsViewModel model = await this.dbContext.Events
                .Where(e => e.Id == eventId)
                .Select(e => new EventDetailsViewModel
                {
                    Id = e.Id,
                    Name = e.Name,
                    Description= e.Description,
                    Start = e.Start.ToString(DateFormat),
                    End = e.End.ToString(DateFormat),
                    Organiser = e.Organiser.UserName,
                    CreatedOn = e.CreatedOn.ToString(DateFormat),
                    Type = e.Type.Name
                })
                .FirstAsync();

            return model;
        }

        public async Task<EventPostModel> GetEventForEditAsync(int id)
        {
            EventPostModel model = await this.dbContext.Events
                .Where(e => e.Id == id)
                .Select(e => new EventPostModel
                {
                    Name = e.Name,
                    Description = e.Description,
                    Start = e.Start.ToString(DateFormat),
                    End = e.End.ToString(DateFormat),
                    TypeId = e.TypeId
                })
                .FirstAsync();

            return model;
        }

        public async Task<IEnumerable<EventAllViewModel>> GetJoinedEvents(string userId)
        {
            IEnumerable<EventAllViewModel> events = await this.dbContext.EventsParticipants
                .Where(ep => ep.HelperId == userId)
                .Select(ep => new EventAllViewModel
                {
                    Id = ep.EventId,
                    Name = ep.Event.Name,
                    Start = ep.Event.Start.ToString(DateFormat),
                    Type = ep.Event.Type.Name,
                    Organiser = ep.Event.Organiser.UserName
                })
                .ToArrayAsync();

            return events;
        }

        public Task<bool> IsEventExistingByIdAsync(int id)
            => this.dbContext.Events.AnyAsync(e => e.Id == id);

        public async Task<bool> IsEventJoinedAsync(int eventId, string userId)
            => await this.dbContext.EventsParticipants.AnyAsync(ep => ep.EventId == eventId && ep.HelperId == userId);

        public async Task<bool> IsUserOwnerOfEventAsync(int id, string userId)
            => await this.dbContext.Events.AnyAsync(e => e.Id == id && e.OrganiserId == userId);

        public async Task JoinEventAsync(int eventId, string userId)
        {
            EventParticipant eventParticipant = new EventParticipant
            {
                EventId = eventId,
                HelperId = userId
            };

            await this.dbContext.EventsParticipants.AddAsync(eventParticipant);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task LeaveEventAsync(int eventId, string userId)
        {
            EventParticipant eventParticipant = await this.dbContext.EventsParticipants
                .FirstAsync(ep => ep.EventId == eventId && ep.HelperId == userId);

            this.dbContext.EventsParticipants.Remove(eventParticipant);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
