using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using RealState.Infrastructure.Identity.Entities;

namespace RealState.Infrastructure.Identity.Context.Configurations
{
    public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("User");

            builder.HasIndex(x => x.IdentifierCard)
                .IsUnique();

            builder.Property(x => x.FirstName);
            builder.Property(x => x.LastName);
            builder.Property(x => x.Picture);
            builder.Property(x => x.IdentifierCard);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.HasMany(x => x.Roles)
                .WithMany(f => f.Users)
                .UsingEntity<IdentityUserRole<string>>();
        }
    }
}
