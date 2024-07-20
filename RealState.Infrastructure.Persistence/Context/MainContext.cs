using Microsoft.EntityFrameworkCore;

namespace RealState.Infrastructure.Persistence.Context
{
    public class MainContext
    : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options) : base(options)
        {
        }
    }
}