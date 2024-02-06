using Homies.ViewModels.Event;

namespace Homies.Services.Interfaces
{
    public interface IEventService
    {
        Task AddEventAsync(EventPostModel model, DateTime startDate, DateTime endDate, string userId);

        Task<IEnumerable<EventAllViewModel>> GetAllEventsAsync();

        Task<EventDetailsViewModel> GetEventDetailsAsync(int eventId);

        Task<bool> IsEventExistingByIdAsync(int id);

        Task<bool> IsUserOwnerOfEventAsync(int id, string userId);

        Task<EventPostModel> GetEventForEditAsync(int id);

        Task EditAsync(EventPostModel model, int eventId, DateTime startDate, DateTime endDate);

        Task<bool> IsEventJoinedAsync(int eventId, string userId);

        Task JoinEventAsync(int eventId, string userId);

        Task<IEnumerable<EventAllViewModel>> GetJoinedEvents(string userId);

        Task LeaveEventAsync(int eventId, string userId);
    }
}
