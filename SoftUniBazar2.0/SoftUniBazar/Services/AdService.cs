namespace SoftUniBazar.Services
{
    using SoftUniBazar.Data;
    using SoftUniBazar.Data.Models;
    using SoftUniBazar.Services.Interfaces;
    using SoftUniBazar.ViewModels.Ad;
    using System.Threading.Tasks;

    public class AdService : IAdService
    {
        private readonly BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task AddNewAdAsync(AdPostModel model, string userId)
        {
            Ad ad = new Ad
            {
                Name = model.Name,
                Description = model.Description,
                ImageUrl = model.ImageUrl,
                Price = model.Price,
                CategoryId = model.CategoryId,
                OwnerId = userId,
                CreatedOn = DateTime.Now
            };

            await this.dbContext.Ads.AddAsync(ad);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
