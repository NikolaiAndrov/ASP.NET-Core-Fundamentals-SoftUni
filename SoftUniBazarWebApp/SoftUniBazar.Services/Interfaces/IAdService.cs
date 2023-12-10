namespace SoftUniBazar.Services.Interfaces
{
	using SoftUniBazar.ViewModels;

	public interface IAdService
	{
		Task<ICollection<ListAllAdViewModel>> GetAllAdsAsync();

		Task<ICollection<CategorySelectViewModel>> GetCategoriesAsync();

		Task AddingNewAdAsync(AdPostViewModel model, string userId);

		Task EditPostAsync(int adId, AdPostViewModel model, string userId);

		Task<AdPostViewModel> GetModelForEditAsync(int adId, string userId);

		Task<ICollection<ListAllAdViewModel>> GetCartAsync(string userId);
	}
}
