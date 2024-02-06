namespace Homies.Services.Interfaces
{
    using Homies.ViewModels.EventType;

    public interface IEventTypeService
    {
        Task<IEnumerable<EventTypeViewModel>> GetAllTypesAsync();

        Task<bool> IsTypeExistingByIdAsync(int typeId);
    }
}
