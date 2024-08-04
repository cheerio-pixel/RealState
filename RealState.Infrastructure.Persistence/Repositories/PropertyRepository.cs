﻿
using Microsoft.EntityFrameworkCore;

using RealState.Application.Extras;
using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository(MainContext context)
    : GenericRepository<Properties>(context), IPropertyRepository
    {
        private readonly DbSet<Properties> _properties = context.Set<Properties>();

        public async Task<bool> IsCodeUnique(string code)
        {
            return !await _properties.AnyAsync(x => x.Code == code);
        }

        public async Task<Properties?> GetByIdWithPictures(Guid id)
        {
            return await _properties.Include(x => x.Pictures).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Properties>> GetPropertyByAgentId(Guid agentId)
        {
            return await _properties.Where(x => x.AgentId == agentId).Include(x => x.Pictures).ToListAsync();
        }

        public Task<int> GetNumberOfPropertiesOfAgent(Guid agentId)
        {
            return _properties.Where(p => p.AgentId == agentId)
                              .Count()
                              .AsTask();
        }

        public async Task<IEnumerable<Guid>> GetPropertyIdsByAgentId(Guid agentId)
        {
            return await (
                from p in _properties
                where p.AgentId == agentId
                select p.Id
            ).ToListAsync();
        }
    }
}
