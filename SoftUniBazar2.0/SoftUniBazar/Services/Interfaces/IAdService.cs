namespace SoftUniBazar.Services.Interfaces
{
    using SoftUniBazar.ViewModels.Ad;

    public interface IAdService 
    {
        Task AddNewAdAsync(AdPostModel model, string userId);

        Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync();

        Task<bool> IsUserOwnerOfAdAsync(string userId, int adId);

        Task<bool> IsAdExistingByIdAsync(int adId);

        Task<AdPostModel> GetAdForEditAsync(int adId);

        Task EditAdAsync(AdPostModel model, int adId);

        Task<bool> IsAdInCartAsync(int adId, string userId);

        Task AddToCartAsync(int adId, string userId);
    }
}
