namespace SoftUniBazar.Services.Interfaces
{
	using SoftUniBazar.ViewModels;

	public interface IAdService
	{
		Task<ICollection<ListAllAdViewModel>> GetAllAdsAsync();
	}
}
