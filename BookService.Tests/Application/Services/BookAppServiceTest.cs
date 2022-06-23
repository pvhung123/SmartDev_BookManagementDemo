using BookService.Application.Domain.Models;
using BookService.Application.Domain.Repository;
using BookService.Application.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BookService.Tests.Application.Services
{
    [TestClass]
    public class BookAppServiceTest
    {
        private readonly Mock<IBookRepository> _mockBookRepository;
        private readonly BookAppService _bookAppService;

        public BookAppServiceTest()
        {
            _mockBookRepository = new Mock<IBookRepository>();

            _bookAppService = new BookAppService(_mockBookRepository.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_ThrowException()
        {
            //Mock
            _mockBookRepository.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());

            await Assert.ThrowsExceptionAsync<Exception>(() => _bookAppService.GetAllAsync());
        }

        [TestMethod]
        public async Task GetAllAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            int id = 1;
            var book = Mock.Of<Book>(x => x.Title == "Title");

            //Mock
            _mockBookRepository.Setup(x => x.GetAsync(id)).ReturnsAsync(book);
            var actionResult = await _bookAppService.GetAsync(id);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Title, book.Title);
        }

        [TestMethod]
        public async Task GetAsync_ThrowException()
        {
            //Arrange
            int id = 1;

            //Mock
            _mockBookRepository.Setup(x => x.GetAsync(id)).ThrowsAsync(new Exception());

            await Assert.ThrowsExceptionAsync<Exception>(() => _bookAppService.GetAsync(id));
        }

        [TestMethod]
        public async Task GetAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var books = new List<Book>
            {
                new Book
                {
                    Title = "Title",
                    Description = "Description"
                }
            };

            //Mock
            _mockBookRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(books);
            var actionResult = await _bookAppService.GetAllAsync();

            //Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public async Task SearchBooksAsync_WhenNullArgument_ThrowArgumentNullException()
        {
            //Arrange
            var exception = new ArgumentNullException("Throw_Exception");

            //Mock
            _mockBookRepository.Setup(x => x.SearchBookAsync(null))
               .ThrowsAsync(exception);

            //Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _bookAppService.SearchBooksAsync(null));
        }

        [TestMethod]
        public async Task SearchBooksAsync_ThrowException()
        {
            //Arrange
            string input = "Test";

            //Mock
            _mockBookRepository.Setup(x => x.SearchBookAsync(input)).ThrowsAsync(new Exception());

            await Assert.ThrowsExceptionAsync<Exception>(() => _bookAppService.SearchBooksAsync(input));
        }


        [TestMethod]
        public async Task SearchBooksAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            string input = "Test";
            
            var books = new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Title = "Title Test",
                    Description = "Description Test"
                }
            };

            //Mock
            _mockBookRepository.Setup(x => x.SearchBookAsync(input)).ReturnsAsync(books);
            var actionResult = await _bookAppService.SearchBooksAsync(input);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Count, books.Count);
            Assert.AreEqual(actionResult[0].Title, books[0].Title);
            Assert.AreEqual(actionResult[0].Description, books[0].Description);
        }
    }
}


