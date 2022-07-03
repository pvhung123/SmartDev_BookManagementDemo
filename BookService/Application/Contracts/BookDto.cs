using BookService.Application.Common.Enum;

namespace BookService.Application.Contracts
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public ReadingStatus ReadingStatus { get; set; }
    }
}
