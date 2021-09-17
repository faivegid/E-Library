using System.Collections.Generic;

namespace GBLAC.API.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Book> Books { get; set; }
    }
}
