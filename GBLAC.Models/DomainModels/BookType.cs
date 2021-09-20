using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GBLAC.Models.Enums
{
    public class BookType
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }
    }
}
