using BookService.Application.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Services
{
    public interface IUserAppService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetAsync(int id);
    }
}
