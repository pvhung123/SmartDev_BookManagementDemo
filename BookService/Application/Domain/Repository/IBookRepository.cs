using BookService.Application.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Domain.Repository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<List<Book>> SearchBookAsync(string title);
        Task<bool> IsBookExistingAsync(string title);
    }
}
