using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GBLAC.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ImageUrl { get; set; }
    }
}
