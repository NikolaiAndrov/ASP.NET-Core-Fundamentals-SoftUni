namespace SeminarHub.Services
{
	using System.Collections.Generic;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;

	using Data;
	using Models.Category;
	using Services.Interfaces;

	public class CategoryService : ICategoryService
	{
		private readonly SeminarHubDbContext dbContext;

        public CategoryService(SeminarHubDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

		public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
		{
			IEnumerable<CategoryViewModel> categories = await this.dbContext.Categories
				.AsNoTracking()
				.Select(c => new CategoryViewModel
				{
					Id = c.Id,
					Name = c.Name
				})
				.ToArrayAsync();

			return categories;
		}

		public async Task<bool> IsCategoryExistingByIdAsync(int id)
			=> await this.dbContext.Categories.AnyAsync(c => c.Id == id);
	}
}
