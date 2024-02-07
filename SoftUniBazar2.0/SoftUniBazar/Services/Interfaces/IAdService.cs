using SoftUniBazar.ViewModels.Ad;

namespace SoftUniBazar.Services.Interfaces
{
    public interface IAdService 
    {
        Task AddNewAdAsync(AdPostModel model, string userId);

        Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync();

        Task<bool> IsUserOwnerOfAdAsync(string userId, int adId);

        Task<bool> IsAdExistingByIdAsync(int adId);

        Task<AdPostModel> GetAdForEditAsync(int adId);
    }
}
