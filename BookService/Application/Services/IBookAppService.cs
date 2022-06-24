using BookService.Application.Contracts;
using BookService.Application.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Services
{
    public interface IBookAppService
    {
        Task<Book> CreateAsync(CreateBookDto createBookDto);
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetAsync(int id);
        Task<List<Book>> SearchBooksAsync(string title);
    }
}
