using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GBLAC.Models
{
    public class Rates
    {
        [Key]
        public string Id { get; set; }
        public Book Book {  get; set; }
        public AppUser User {  get; set; }
        public int Rating { get; set; }

        public Rates()
        {
            Id = Guid.NewGuid().ToString();
        }
    }
}
