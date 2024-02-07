namespace SoftUniBazar.Services
{
    using Microsoft.EntityFrameworkCore;
    using SoftUniBazar.Data;
    using SoftUniBazar.Services.Interfaces;
    using SoftUniBazar.ViewModels.Category;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CategoryService : ICategoryService
    {
        private readonly BazarDbContext dbContext;

        public CategoryService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<CategoryViewModel>> GetAllCategoriesAsync()
        {
            IEnumerable<CategoryViewModel> categories = await this.dbContext.Categories
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
