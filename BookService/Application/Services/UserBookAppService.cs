using BookService.Application.Common.Enum;
using BookService.Application.Common.Exceptions;
using BookService.Application.Contracts;
using BookService.Application.Domain.Models;
using BookService.Application.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Services
{
    public class UserBookAppService : IUserBookAppService
    {
        private readonly IUserBookRepository _userBookRepository;

        public UserBookAppService(IUserBookRepository userBookRepository)
        {
            _userBookRepository = userBookRepository;
        }

        public async Task<UserBook> AddUserBookAsync(CreateUserBookDto createUserBookDto)
        {
            if (createUserBookDto == null)
                throw new ArgumentNullException(ExceptionCode.EX_1001_OBJECT_NULL, ExceptionMessage.EX_1001_OBJECT_NULL);
            
            try
            {
                var isExisting = await _userBookRepository.IsUserBookExistingAsync(
                    createUserBookDto.BookId,
                    createUserBookDto.UserId);

                if (isExisting) 
                    throw new Exception(ExceptionMessage.EX_1002_BOOK_EXISTED);

                UserBook userBook = new UserBook
                {
                    UserId = createUserBookDto.UserId,
                    BookId = createUserBookDto.BookId,
                    ReadingStatus = (int)ReadingStatus.Open
                };

                var result = await _userBookRepository.InsertAsync(userBook);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserBook>> GetBooksCompletedAsync(int userId)
        {            
            try
            {
                var books = await _userBookRepository.GetBooksByStatusAsync(userId, (int)ReadingStatus.Completed);

                return books;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<UserBook>> GetBooksByUserIdAsync(int userId)
        {
            try
            {
                var books = await _userBookRepository.GetBooksByUserIdAsync(userId);

                return books;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }        
    }
}
