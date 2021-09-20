using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GBLAC.Data.DatabaseConfiguration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                    new IdentityRole { Name = "admin", NormalizedName = "ADMIN" },
                    new IdentityRole { Name = "customer", NormalizedName = "CUSTOMER" }
                );
        }
    }
}
