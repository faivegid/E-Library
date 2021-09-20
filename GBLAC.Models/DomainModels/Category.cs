using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GBLAC.Models
{
    public class Category
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public List<Book> Books { get; set; }

        public Category()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
