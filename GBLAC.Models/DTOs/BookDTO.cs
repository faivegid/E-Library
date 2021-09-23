using GBLAC.Models;
using GBLAC.Models.Enums;
using System;
using System.Collections.Generic;

namespace GBLAC.Services.APIServices.Interfaces
{
    public class BookDTO
    {
        public string Id { get; set; }
        public string BookName { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string Description { get; set; }
        public string ISBN { get; set; }
        public string AuthorName { get; set; }
        public string PublisherName { get; set; }
        public DateTime DateReleased { get; set; }
        public DateTime DateAdded { get; set; }
        public List<BookType> BookTypes { get; set; }
        public List<Rates> Ratings { get; set; }
        public List<Category> Category { get; set; }
    }
}