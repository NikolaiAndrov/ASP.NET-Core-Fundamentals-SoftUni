namespace Homies.Services.Interfaces
{
	using Homies.ViewModels;

	public interface IEventService
	{
		Task<ICollection<TypePostModel>> GetAllTypesForEventAsync();

		Task AddEventAsync(EventPostModel model, string userId);
	}
}
