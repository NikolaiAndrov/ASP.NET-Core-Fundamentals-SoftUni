namespace SoftUniBazar.Services
{
    using Microsoft.EntityFrameworkCore;
    using SoftUniBazar.Data;
    using SoftUniBazar.Data.Models;
    using SoftUniBazar.Services.Interfaces;
    using SoftUniBazar.ViewModels.Ad;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using static Common.GeneralApplicationConstants;

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

        public async Task<AdPostModel> GetAdForEditAsync(int adId)
        {
            AdPostModel model = await this.dbContext.Ads
                .Select(ad => new AdPostModel
                {
                    Name = ad.Name,
                    Description = ad.Description,
                    ImageUrl = ad.ImageUrl,
                    Price = ad.Price,
                    CategoryId = ad.CategoryId,
                })
                .FirstAsync();

            return model;
        }

        public async Task<IEnumerable<AdAllViewModel>> GetAllAdsAsync()
        {
            IEnumerable<AdAllViewModel> ads = await this.dbContext.Ads
                .Select(a => new AdAllViewModel
                {
                    Id = a.Id,
                    Name = a.Name,
                    ImageUrl= a.ImageUrl,
                    CreatedOn = a.CreatedOn.ToString(DateFormat),
                    Category = a.Category.Name,
                    Description = a.Description,
                    Price = a.Price,
                    Owner = a.Owner.UserName
                })
                .ToArrayAsync();

            return ads;
        }

        public async Task<bool> IsAdExistingByIdAsync(int adId)
            => await this.dbContext.Ads.AnyAsync(a => a.Id == adId);

        public async Task<bool> IsUserOwnerOfAdAsync(string userId, int adId)
            => await this.dbContext.Ads.AnyAsync(a => a.OwnerId == userId && a.Id == adId);
    }
}
