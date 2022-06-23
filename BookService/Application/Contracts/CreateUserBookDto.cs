using BookService.Application.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Contracts
{
    public class CreateUserBookDto
    {
        [Required]
        public int UserId { get; set; }
        [Required]
        public int BookId { get; set; }

        public ReadingStatus ReadingStatus { get; set; }
    }
}
