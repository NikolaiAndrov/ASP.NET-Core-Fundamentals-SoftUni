namespace SoftUniBazar.Services
{
	using Microsoft.EntityFrameworkCore;
	using SoftUniBazar.Data;
	using SoftUniBazar.Services.Interfaces;
	using SoftUniBazar.ViewModels;

	public class AdService : IAdService
	{
		private readonly BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<ICollection<ListAllAdViewModel>> GetAllAdsAsync()
		{
			string dateFormat = "yyyy-MM-dd H:mm";

			ICollection<ListAllAdViewModel> allAds = await dbContext.Ads
				.Select(a => new ListAllAdViewModel
				{
					Id = a.Id,
					Name = a.Name,
					ImageUrl = a.ImageUrl,
					CreatedOn = a.CreatedOn.ToString(dateFormat),
					Category = a.Category.Name,
					Description = a.Description,
					Price = a.Price.ToString("f2"),
					Owner = a.Owner.UserName
				})
				.ToArrayAsync();

			return allAds;
		}
	}
}
