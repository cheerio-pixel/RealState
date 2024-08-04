using Microsoft.EntityFrameworkCore;

using RealState.Application.DTOs.SalesType;
using RealState.Application.Interfaces.Repositories;
using RealState.Application.QueryFilters;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    internal class SalesTypeRepository
    : GenericRepository<SalesTypes>, ISalesTypeRepository
    {
        private readonly MainContext _context;

        public SalesTypeRepository(MainContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> DoesSalesTypeNameExists(string name, Guid? idToExclude)
        {
            return await PropertyExistsWithValue(e => e.Name, name, idToExclude.GetValueOrDefault());
        }

        public IQueryable<SalesTypes> Filter(SalesTypesQueryFilter filter)
        {
            IQueryable<SalesTypes> salesTypeses = _context.SalesTypes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                salesTypeses = salesTypeses.Where(st => st.Name.Contains(filter.Name));
            }
            return salesTypeses;
        }

        public async Task<List<SalesTypes>> ListSalesTypes(SalesTypesQueryFilter filter)
        {
            return await Filter(filter).ToListAsync();
        }

        public async Task<List<SalesTypesListItemDTO>> ListSalesTypesWithCount(SalesTypesQueryFilter filter)
        {
            IQueryable<SalesTypes> salesTypeses = Filter(filter);

            return await salesTypeses.Select(st => new SalesTypesListItemDTO()
            {
                Id = st.Id,
                Name = st.Name,
                Description = st.Description,
                NumberOfProperties = st.Properties.Count
            }).ToListAsync();
        }
    }
}