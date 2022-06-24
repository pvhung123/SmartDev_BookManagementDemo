using BookService.Application.Domain.Models;
using BookService.Application.Domain.Repository;

namespace BookService.Application.Data
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(BookManagementContext dbContext) : base(dbContext)
        {
        }
    }
}
