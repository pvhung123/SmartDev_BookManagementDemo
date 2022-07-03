using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Contracts
{
    public class UserBookDto
    {
        [Required]
        public int UserId { get; set; }        
        public List<BookDto> Books { get; set; }
    }
}
