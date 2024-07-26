using RealState.Application.Interfaces.Repositories;
using RealState.Domain.Entities;
using RealState.Infrastructure.Persistence.Context;

namespace RealState.Infrastructure.Persistence.Repositories
{
    public class PictureRepository(MainContext context) : GenericRepository<Pictures>(context), IPictureRepository
    {

        
    }
}
