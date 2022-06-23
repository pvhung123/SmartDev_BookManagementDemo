using BookService.Application.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Domain.Repository
{
    public interface IUserBookRepository : IRepository<UserBook>
    {
        Task<List<UserBook>> GetBooksByUserIdAsync(int userId);
        Task<List<UserBook>> GetBooksByStatusAsync(int userId, int readingStatus);
        Task<UserBook> GetUserBookAsync(int bookId, int userId);
        Task<bool> IsUserBookExistingAsync(int bookId, int userId);
    }
}
