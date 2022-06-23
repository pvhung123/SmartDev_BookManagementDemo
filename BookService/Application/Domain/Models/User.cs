using BookService.Application.Domain.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Domain.Models

{
    public class User : Entity
    {
        [Required]
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Address { get; set; }

        public ICollection<UserBook> UserBooks { get; set; }
    }
}
