using System;
using System.Collections.Generic;

namespace GBLAC.API.Models
{
    public class Book
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public ICollection<Type> Type { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public DateTime DateReleased { get; set; }
        public DateTime DateAdded { get; set; }
        public List<int> Ratings { get; set; }
        public ICollection<Category> Category { get; set; }
    }
}
