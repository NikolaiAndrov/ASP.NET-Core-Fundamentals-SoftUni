namespace Homies.Services
{
    using Homies.Data;
    using Homies.Services.Interfaces;
    using Homies.ViewModels.EventType;
    using Microsoft.EntityFrameworkCore;

    public class EventTypeService : IEventTypeService
    {
        private readonly HomiesDbContext dbContext;

        public EventTypeService(HomiesDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<IEnumerable<EventTypeViewModel>> GetAllTypesAsync()
        {
            IEnumerable<EventTypeViewModel> eventTypes = await this.dbContext.Types
                .Select(t => new EventTypeViewModel
                {
                    Id = t.Id,
                    Name = t.Name
                })
                .ToListAsync();

            return eventTypes;
        }

        public async Task<bool> IsTypeExistingByIdAsync(int typeId)
            => await this.dbContext.Types.AnyAsync(t => t.Id == typeId);
    }
}
