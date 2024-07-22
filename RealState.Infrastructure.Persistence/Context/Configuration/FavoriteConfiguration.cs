using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Domain.Entities;

namespace RealState.Infrastructure.Persistence.Context.Configuration
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {
            builder.HasOne(p => p.Property)
                .WithMany(p => p.Favorites)
                .HasForeignKey(p => p.PropertyId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
