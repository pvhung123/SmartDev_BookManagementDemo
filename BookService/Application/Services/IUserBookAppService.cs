using BookService.Application.Contracts;
using BookService.Application.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Services
{
    public interface IUserBookAppService
    {
        Task<UserBook> AddUserBookAsync(CreateUserBookDto createUserBookDto);

        Task<List<UserBook>> GetBooksByUserIdAsync(int userId);

        Task<List<UserBook>> GetBooksCompletedAsync(int userId);
    }
}
