namespace Homies.Services
{
	using Homies.Data;
	using Homies.Data.Models;
	using Homies.Services.Interfaces;
	using Homies.ViewModels;
	using Microsoft.EntityFrameworkCore;
	using System.Globalization;

	public class EventService : IEventService
	{
		private readonly HomiesDbContext dbContext;

		public EventService(HomiesDbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		public async Task AddEventAsync(EventPostModel model, string userId)
		{
			DateTime start = DateTime.Parse(model.Start, CultureInfo.InvariantCulture);
			DateTime end = DateTime.Parse(model.End, CultureInfo.InvariantCulture);

			Event eventToAdd = new Event
			{
				Name = model.Name,
				Description = model.Description,
				CreatedOn = DateTime.Now,
				Start = start,
				End = end,
				OrganiserId = userId,
				TypeId = model.TypeId,
			};

			await dbContext.Events.AddAsync(eventToAdd);
			await dbContext.SaveChangesAsync();
		}

		public async Task EditEventAsync(int eventId, EventPostModel eventPost, string userId)
		{
			Event eventToEdit = await dbContext.Events
				.FirstAsync(e => e.Id == eventId);

			if (eventToEdit.OrganiserId != userId)
			{
				throw new InvalidOperationException();
			}

			DateTime start = DateTime.Parse(eventPost.Start, CultureInfo.InvariantCulture);
			DateTime end = DateTime.Parse(eventPost.End, CultureInfo.InvariantCulture);

			eventToEdit.Name = eventPost.Name;
			eventToEdit.Description = eventPost.Description;
			eventToEdit.Start = start;
			eventToEdit.End = end;
			eventToEdit.TypeId = eventPost.TypeId;

			await dbContext.SaveChangesAsync();
		}

		public async Task<ICollection<EventAllViewModel>> GetAllEventsAsync()
		{
			string dateFormat = "yyyy-MM-dd H:mm";

			ICollection<EventAllViewModel> events = await dbContext.Events
				.Select(e => new EventAllViewModel
				{
					Id = e.Id,
					Name = e.Name,
					Start = e.Start.ToString(dateFormat),
					Type = e.Type.Name,
					Organiser = e.Organiser.UserName
				})
				.ToArrayAsync();

			return events;
		}

		public async Task<ICollection<TypePostModel>> GetAllTypesForEventAsync()
		{
			ICollection<TypePostModel> types = await dbContext.Types
				.Select( t => new TypePostModel
				{
					Id = t.Id,
					Name = t.Name,
				})
				.ToArrayAsync();

			return types;
		}

		public async Task<EventDetailsViewModel> GetEventDetailsAsync(int eventId)
		{
			string dateFormat = "yyyy-MM-dd H:mm";

			EventDetailsViewModel eventDetails = await dbContext.Events
				.Where(e => e.Id == eventId)
				.Select(e => new EventDetailsViewModel
				{
					Id = e.Id,
					Name = e.Name,
					Description = e.Description,
					Start = e.Start.ToString(dateFormat),
					End = e.End.ToString(dateFormat),
					Organiser = e.Organiser.UserName,
					CreatedOn = e.CreatedOn.ToString(dateFormat),
					Type = e.Type.Name,
				})
				.FirstAsync();

			return eventDetails;
		}

		public async Task<EventPostModel> GetEventForEditAsync(int eventId, string userId)
		{
			Event eventToEdit = await dbContext.Events
				.FirstAsync(e => e.Id == eventId);

			if (eventToEdit.OrganiserId != userId)
			{
				throw new InvalidCastException();
			}

			EventPostModel eventPost = new EventPostModel
			{
				Name = eventToEdit.Name,
				Description= eventToEdit.Description,
				Start = eventToEdit.Start.ToString(),
				End = eventToEdit.End.ToString(),
				TypeId = eventToEdit.TypeId
			};

			return eventPost;
		}
	}
}
