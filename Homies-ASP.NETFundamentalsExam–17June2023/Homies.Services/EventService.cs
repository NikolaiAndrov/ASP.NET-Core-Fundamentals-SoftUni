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
			string dateFormat = @"yyyy-MM-dd H:mm";
			DateTime start = DateTime.ParseExact(model.Start, dateFormat, CultureInfo.InvariantCulture);
			DateTime end = DateTime.ParseExact(model.End, dateFormat, CultureInfo.InvariantCulture);

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
	}
}
