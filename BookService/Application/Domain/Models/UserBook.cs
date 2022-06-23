

using BookService.Application.Domain.Repository;
using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Domain.Models
{
    public class UserBook : Entity
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }        
        public int ReadingStatus { get; set; }

        public User User { get; set; }
        public Book Book { get; set; }
    }
}
