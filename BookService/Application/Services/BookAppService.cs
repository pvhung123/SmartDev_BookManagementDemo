using BookService.Application.Common.Exceptions;
using BookService.Application.Contracts;
using BookService.Application.Domain.Models;
using BookService.Application.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Application.Services
{
    public class BookAppService : IBookAppService
    {
        private readonly IBookRepository _bookRepository;

        public BookAppService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<Book> CreateAsync(CreateBookDto createBookDto)
        {
            if (createBookDto == null)
                throw new ArgumentNullException(ExceptionCode.EX_1001_OBJECT_NULL, ExceptionMessage.EX_1001_OBJECT_IS_NULL);

            try
            {
                var isExisting = await _bookRepository.IsBookExistingAsync(createBookDto.Title);
                    

                if (isExisting)
                    throw new Exception(ExceptionMessage.EX_1002_BOOK_EXISTED);

                Book book = new Book
                {
                    Title = createBookDto.Title,
                    Author = createBookDto.Author,
                    Description = createBookDto.Description
                };

                var result = await _bookRepository.InsertAsync(book);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {            
            try
            {
                var books = await _bookRepository.GetAllAsync();

                return books;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<Book> GetAsync(int id)
        {
            try
            {
                var book = await _bookRepository.GetAsync(id);

                return book;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Book>> SearchBooksAsync(string title)
        {
            if (title == null)
                throw new ArgumentNullException(ExceptionCode.EX_1001_OBJECT_NULL, ExceptionMessage.EX_1001_OBJECT_IS_NULL);

            try
            {
                var books = await _bookRepository.SearchBookAsync(title);

                return books;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
