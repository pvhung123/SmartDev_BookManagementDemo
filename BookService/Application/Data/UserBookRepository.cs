using BookService.Application.Domain.Models;
using BookService.Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Application.Data
{
    public class UserBookRepository : Repository<UserBook>, IUserBookRepository
    {
        public UserBookRepository(BookManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<List<UserBook>> GetBooksByStatusAsync(int userId, int readingStatus)
        {
            return await DbSet
                .Include(b => b.Book)
                .Where(ub => ub.UserId == userId && ub.ReadingStatus == readingStatus)
                .ToListAsync();
        }

        public async Task<List<UserBook>> GetBooksByUserIdAsync(int userId)
        {
            return await DbSet
                .Include(b => b.Book)
                .Where(ub => ub.UserId == userId)
                .ToListAsync(); ;
        }

        public async Task<UserBook> GetUserBookAsync(int bookId, int userId)
        {
            return await DbSet.FirstOrDefaultAsync(ub => ub.BookId == bookId && ub.UserId == userId);            
        }

        public async Task<bool> IsUserBookExistingAsync(int bookId, int userId)
        {
            return await DbSet.AnyAsync(ub => ub.BookId == bookId && ub.UserId == userId);
        }
    }
}
