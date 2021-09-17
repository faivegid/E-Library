using GBLAC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Data
{

    public class GBlacContext : IdentityDbContext<AppUser>
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public GBlacContext(DbContextOptions<GBlacContext> options) : base(options)
        {
        }
    }
}
