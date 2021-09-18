using System.Collections.Generic;

namespace GBLAC.Models
{
    public class Rates
    {
        public string Id { get; set; }
        public Book Book {  get; set; }
        public AppUser User {  get; set; }
        public int Rating { get; set; }
    }
}
