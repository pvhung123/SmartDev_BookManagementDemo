using BookService.Application.Contracts;
using BookService.Application.Domain.Models;
using BookService.Application.Services;
using BookService.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;

namespace BookService.Tests.Controllers
{
    [TestClass]
    public class BookControllerTest
    {
        private readonly Mock<IBookAppService> _mockBookAppService;
        private readonly BookController _bookController;

        public BookControllerTest()
        {
            _mockBookAppService = new Mock<IBookAppService>();
            _bookController = new BookController(_mockBookAppService.Object);
        }

        [TestMethod]
        public async Task CreateAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var createBookDto = Mock.Of<CreateBookDto>(x => x.Title == "Title");

            var book = Mock.Of<Book>(x => x.Title == "Title");

            //Mock
            _mockBookAppService.Setup(x => x.CreateAsync(It.IsAny<CreateBookDto>())).ReturnsAsync(book);
            var actionResult = await _bookController.CreateAsync(createBookDto);

            //Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public async Task CreateAsync_WhenNullArgurment_ReturnBadRequest()
        {
            //Act
            var actionResult = (ObjectResult)await _bookController.CreateAsync(null);

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAllAsync_WhenValidArgument_ReturnOkResult()
        {
            //Act
            var actionResult = await _bookController.GetAllAsync();

            //Assert
            _mockBookAppService.Verify(a => a.GetAllAsync(), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);                  
        }

        [TestMethod]
        public async Task GetAllAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            _mockBookAppService.Setup(a => a.GetAllAsync()).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _bookController.GetAllAsync();

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            int id = 1;
            //Act
            var actionResult = await _bookController.GetAsync(id);

            //Assert
            _mockBookAppService.Verify(a => a.GetAsync(id), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            _mockBookAppService.Setup(a => a.GetAsync(It.IsAny<int>())).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _bookController.GetAsync(It.IsAny<int>());

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task SearchBooksAsync_WhenNullArgurment_ReturnBadRequest()
        {
            //Act
            var actionResult = (ObjectResult)await _bookController.SearchBooksAsync(null);

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.BadRequest, actionResult.StatusCode);

        }
        [TestMethod]
        public async Task SearchBooksAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var bookSearchDto = Mock.Of<BookSearchDto>(b => b.Title == "test");

            //ActB
            var actionResult = await _bookController.SearchBooksAsync(bookSearchDto);

            //Assert
            _mockBookAppService.Verify(a => a.SearchBooksAsync(bookSearchDto.Title), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);
        }

        [TestMethod]
        public async Task SearchBooksAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            var bookSearchDto = Mock.Of<BookSearchDto>(b => b.Title == "test");

            _mockBookAppService.Setup(a => a.SearchBooksAsync(bookSearchDto.Title)).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _bookController.SearchBooksAsync(bookSearchDto);

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }
    }
}
