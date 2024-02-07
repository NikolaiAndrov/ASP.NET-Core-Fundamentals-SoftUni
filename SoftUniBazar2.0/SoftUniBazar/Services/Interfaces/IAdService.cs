using SoftUniBazar.ViewModels.Ad;

namespace SoftUniBazar.Services.Interfaces
{
    public interface IAdService 
    {
        Task AddNewAdAsync(AdPostModel model, string userId);
    }
}
