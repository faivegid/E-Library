using System.Collections.Generic;

namespace GBLAC.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
