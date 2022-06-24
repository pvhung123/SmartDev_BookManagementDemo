using System.ComponentModel.DataAnnotations;

namespace BookService.Application.Contracts
{
    public class CreateBookDto
    {
        [Required]
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
    }
}
