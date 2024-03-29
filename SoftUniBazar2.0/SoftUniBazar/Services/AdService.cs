﻿namespace SoftUniBazar.Services
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

        public async Task AddToCartAsync(int adId, string userId)
        {
            AdBuyer adBuyer = new AdBuyer
            {
                AdId = adId,
                BuyerId = userId
            };

            await this.dbContext.AdsBuyers.AddAsync(adBuyer);
            await this.dbContext.SaveChangesAsync();
        }

        public async Task EditAdAsync(AdPostModel model, int adId)
        {
            Ad ad = await this.dbContext.Ads
                .FirstAsync(ad => ad.Id == adId);

            ad.Name = model.Name;
            ad.Description = model.Description;
            ad.ImageUrl = model.ImageUrl;
            ad.Price = model.Price;
            ad.CategoryId = model.CategoryId;

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

        public async Task<IEnumerable<AdAllViewModel>> GetCartElementsAsync(string userId)
        {
            IEnumerable<AdAllViewModel> ads = await this.dbContext.AdsBuyers
                .Where(a => a.BuyerId == userId)
                .Select(a => new AdAllViewModel
                {
                    Id = a.AdId,
                    Name = a.Ad.Name,
                    ImageUrl = a.Ad.ImageUrl,
                    CreatedOn = a.Ad.CreatedOn.ToString(DateFormat),
                    Category = a.Ad.Category.Name,
                    Description = a.Ad.Description,
                    Price = a.Ad.Price,
                    Owner = a.Ad.Owner.UserName
                })
                .ToArrayAsync();

            return ads;
        }

        public async Task<bool> IsAdExistingByIdAsync(int adId)
            => await this.dbContext.Ads.AnyAsync(a => a.Id == adId);

        public async Task<bool> IsAdInCartAsync(int adId, string userId)
            => await this.dbContext.AdsBuyers.AnyAsync(ab => ab.BuyerId == userId && ab.AdId == adId);

        public async Task<bool> IsUserOwnerOfAdAsync(string userId, int adId)
            => await this.dbContext.Ads.AnyAsync(a => a.OwnerId == userId && a.Id == adId);

        public async Task RemoveFromCartAsync(int adId, string userId)
        {
            AdBuyer adBuyer = await this.dbContext.AdsBuyers
                .FirstAsync(ab => ab.BuyerId == userId && ab.AdId == adId);

            this.dbContext.AdsBuyers.Remove(adBuyer);
            await this.dbContext.SaveChangesAsync();
        }
    }
}
