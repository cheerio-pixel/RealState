using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository(MainContext context) : GenericRepository<Properties>(context), IPropertyRepository
    {
        private readonly DbSet<Properties> _properties = context.Set<Properties>();

        public async Task<bool> IsCodeUnique(string code)
        {
            return !await _properties.AnyAsync(x => x.Code != code);
        }

        public async Task<Properties?> GetByIdWithPictures(Guid id)
        {
            return await _properties.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
