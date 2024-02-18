namespace SeminarHub.Services.Interfaces
{
	using Models.Category;

	public interface ICategoryService
	{
		Task<bool> IsCategoryExistingByIdAsync(int id);

		Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync();
	}
}
