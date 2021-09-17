using GBLAC.API.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace GBLAC.API.DataContext
{
    public class GBlacContext:IdentityDbContext<AppUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Type> Types { get; set; }
        public GBlacContext(DbContextOptions<GBlacContext> options):base(options)
        {
        }
    }
}
