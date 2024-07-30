using Microsoft.EntityFrameworkCore;

using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PictureRepository(MainContext context) : GenericRepository<Pictures>(context), IPictureRepository
    {
    private readonly DbSet<Pictures> _pictures = context.Set<Pictures>();
    
        public async Task<List<Pictures>> GetAllByPropertyId(Guid propertyId)
        {
            return await _pictures.Where(x => x.PropertyId == propertyId).ToListAsync();
        }
    }
}
