
using BookService.Application.Domain.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Domain.Models
{
    public class Book : Entity
    {
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }
    }
}
