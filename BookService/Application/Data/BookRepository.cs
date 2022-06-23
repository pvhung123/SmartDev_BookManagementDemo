using BookService.Application.Domain.Models;
using BookService.Application.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookService.Application.Data
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(BookManagementContext dbContext) : base(dbContext)
        {
        }

        public async Task<bool> IsBookExistingAsync(string title)
        {
            return await DbSet.AnyAsync(b => b.Title == title);
        }

        public async Task<List<Book>> SearchBookAsync(string title)
        {
            return await DbSet.Where(b => b.Title.Contains(title)).ToListAsync();
        }
    }
}
