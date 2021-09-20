using GBLAC.Data.DatabaseConfiguration;
using GBLAC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GBLAC.Data
{

    public class GBlacContext : IdentityDbContext<AppUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Rates> Ratings { get; set; }
        public GBlacContext(DbContextOptions<GBlacContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfiguration(new RoleConfiguration());
        }
    }
}
