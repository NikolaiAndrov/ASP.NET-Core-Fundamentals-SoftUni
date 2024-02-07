namespace SoftUniBazar.Services
{
    using SoftUniBazar.Data;
    using SoftUniBazar.Services.Interfaces;

    public class AdService : IAdService
    {
        private readonly BazarDbContext dbContext;

        public AdService(BazarDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
    }
}
