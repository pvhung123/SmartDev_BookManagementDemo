using BookService.Application.Common.Exceptions;
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


        public async Task<IEnumerable<Book>> GetAllAsync()
        {            
            try
            {
                var books = await _bookRepository.GetAllAsync();

                return books;
            }
            catch (Exception ex)
            {
                throw;
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
                throw;
            }
        }

        public async Task<List<Book>> SearchBooksAsync(string title)
        {
            if (title == null)
                throw new ArgumentNullException(ExceptionCode.EX_1001_OBJECT_NULL, ExceptionMessage.EX_1001_OBJECT_NULL);

            try
            {
                var books = await _bookRepository.SearchBookAsync(title);

                return books;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
