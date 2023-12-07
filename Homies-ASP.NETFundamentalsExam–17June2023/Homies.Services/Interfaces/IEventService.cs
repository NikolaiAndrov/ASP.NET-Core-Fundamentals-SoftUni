namespace Homies.Services.Interfaces
{
	using Homies.ViewModels;

	public interface IEventService
	{
		Task<ICollection<TypePostModel>> GetAllTypesForEventAsync();

		Task AddEventAsync(EventPostModel model, string userId);

		Task<ICollection<EventAllViewModel>> GetAllEventsAsync();

		Task<EventDetailsViewModel> GetEventDetailsAsync(int eventId);

		Task<EventPostModel> GetEventForEditAsync(int eventId, string userId);
		Task EditEventAsync(int eventId, EventPostModel eventPost, string userId);

		Task JoinEventAsync(int eventId, string userId);

		Task<ICollection<EventAllViewModel>> ViewJoinedEventsAsync(string userId);

		Task LeaveEventAsync(int eventId, string userId);
	}
}
