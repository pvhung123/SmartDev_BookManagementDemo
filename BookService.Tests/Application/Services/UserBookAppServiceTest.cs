using BookService.Application.Common.Enum;
using BookService.Application.Contracts;
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
    public class UserBookAppServiceTest
    {
        private readonly Mock<IUserBookRepository> _mockUserBookRepository;
        private readonly UserBookAppService _userBookAppService;

        public UserBookAppServiceTest()
        {
            _mockUserBookRepository = new Mock<IUserBookRepository>();

            _userBookAppService = new UserBookAppService(_mockUserBookRepository.Object);
        }

        [TestMethod]
        public async Task AddUserBookAsync_WhenNullArgument_ThrowArgumentNullException()
        {
            //Arrange
            var exception = new ArgumentNullException("Throw_Exception");

            //Mock
            _mockUserBookRepository.Setup(x => x.InsertAsync(null))
               .ThrowsAsync(exception);

            //Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userBookAppService.AddUserBookAsync(null));
        }

        [TestMethod]
        public async Task AddUserBookAsync_WhenBooksNull_ThrowArgumentNullException()
        {
            //Arrange
            var exception = new ArgumentNullException("Throw_Exception");
            var userBookDto = new UserBookDto();

            //Mock
            _mockUserBookRepository.Setup(x => x.InsertAsync(It.IsAny<UserBook>()))
               .ThrowsAsync(exception);

            //Assert
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => _userBookAppService.AddUserBookAsync(userBookDto));
        }

        [TestMethod]
        public async Task AddUserBookAsync_WhenBookExisted_ThrowException()
        {
            //Arrange
            var bookId = 1;
            var userBookDto = new UserBookDto
            {
                UserId = 1,
                Books = new List<BookDto>()
                {
                    new BookDto
                    {
                        Id = bookId,
                        Title = "Test",
                        ReadingStatus = ReadingStatus.Completed
                    }
                }
            };


            var exception = new Exception("This book already existed.");

            //Mock
            _mockUserBookRepository.Setup(x => x.IsUserBookExistingAsync(userBookDto.UserId, bookId)).ReturnsAsync(true);

            //Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _userBookAppService.AddUserBookAsync(userBookDto));
        }

        [TestMethod]
        public async Task AddUserBookAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var bookId = 1;
            var userBookDto = new UserBookDto
            {
                UserId = 1,
                Books = new List<BookDto>()
                {
                    new BookDto
                    {
                        Id = bookId,
                        Title = "Test",
                        ReadingStatus = ReadingStatus.Completed
                    }
                }
            };

            var userBook = Mock.Of<UserBook>(x => x.UserId == 1 && x.BookId == 1);

            //Mock
            _mockUserBookRepository.Setup(x => x.InsertAsync(It.IsAny<UserBook>())).ReturnsAsync(userBook);
            var actionResult = await _userBookAppService.AddUserBookAsync(userBookDto);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(userBookDto.UserId, actionResult.UserId);
            Assert.AreEqual(userBookDto.Books[0].Id, actionResult.BookId);
        }

        [TestMethod]
        public async Task AddUserBookAsync_ThrowException()
        {
            //Arrange
            var exception = new Exception("Throw_Exception");
            //Arrange
            var input = new UserBookDto
            {
                UserId = 1,
                Books = new List<BookDto>()
                {
                    new BookDto
                    {
                        Id = 1,
                        Title = "Test",
                        ReadingStatus = ReadingStatus.Completed
                    }
                }
            };

            //Mock
            _mockUserBookRepository.Setup(x => x.InsertAsync(It.IsAny<UserBook>()))
               .ThrowsAsync(exception);

            //Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _userBookAppService.AddUserBookAsync(input));
        }

        [TestMethod]
        public async Task GetBooksCompletedAsync_ThrowException()
        {
            //Mock
            _mockUserBookRepository.Setup(x => x.GetBooksByStatusAsync(It.IsAny<int>(), It.IsAny<int>())).ThrowsAsync(new Exception());
            
            await Assert.ThrowsExceptionAsync<Exception>(() => _userBookAppService.GetBooksCompletedAsync(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task GetBooksCompletedAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var userBooks = new List<UserBook>
            {
                new UserBook 
                { 
                    BookId = 1,
                    UserId = 1,
                    ReadingStatus = (int)ReadingStatus.Completed
                }
            };
            
            //Mock
            _mockUserBookRepository.Setup(x => x.GetBooksByStatusAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(userBooks);
            var actionResult = await _userBookAppService.GetBooksCompletedAsync(It.IsAny<int>());

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Count, userBooks.Count);
            Assert.AreEqual(actionResult[0].ReadingStatus, userBooks[0].ReadingStatus);
        }

        [TestMethod]
        public async Task GetBooksByUserIdAsync_ThrowException()
        {
            //Mock
            _mockUserBookRepository.Setup(x => x.GetBooksByUserIdAsync(It.IsAny<int>())).ThrowsAsync(new Exception());

            await Assert.ThrowsExceptionAsync<Exception>(() => _userBookAppService.GetBooksByUserIdAsync(It.IsAny<int>()));
        }

        [TestMethod]
        public async Task GetBooksByUserIdAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var userBooks = new List<UserBook>
            {
                new UserBook
                {
                    BookId = 1,
                    UserId = 1,
                    ReadingStatus = (int)ReadingStatus.Open
                }
            };

            //Mock
            _mockUserBookRepository.Setup(x => x.GetBooksByUserIdAsync(It.IsAny<int>())).ReturnsAsync(userBooks);
            var actionResult = await _userBookAppService.GetBooksByUserIdAsync(It.IsAny<int>());

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Count, userBooks.Count);
            Assert.AreEqual(actionResult[0].ReadingStatus, userBooks[0].ReadingStatus);
        }        
    }
}


