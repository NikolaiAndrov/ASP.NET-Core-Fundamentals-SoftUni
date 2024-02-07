namespace SoftUniBazar.Services.Interfaces
{
    using SoftUniBazar.ViewModels.Category;

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();

        Task<bool> IsCategoryExistingByIdAsync(int id);
    }
}
