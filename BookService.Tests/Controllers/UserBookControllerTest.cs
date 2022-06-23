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
    public class UserBookControllerTest
    {
        private readonly Mock<IUserBookAppService> _mockUserBookAppService;
        private readonly UserBookController _userBookController;

        public UserBookControllerTest()
        {
            _mockUserBookAppService = new Mock<IUserBookAppService>();
            _userBookController = new UserBookController(_mockUserBookAppService.Object);
        }

        [TestMethod]
        public async Task AddUserBookAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var createUserBookDto = Mock.Of<CreateUserBookDto>(
                x => x.UserId == 1 && x.BookId == 1);

            var userBook = Mock.Of<UserBook>(x => x.UserId == 1 && x.BookId == 1);

            //Mock
            _mockUserBookAppService.Setup(x => x.AddUserBookAsync(It.IsAny<CreateUserBookDto>())).ReturnsAsync(userBook);
            var actionResult = await _userBookController.AddUserBookAsync(createUserBookDto);

            //Assert
            Assert.IsNotNull(actionResult);
        }

        [TestMethod]
        public async Task AddUserBookAsync_WhenNullArgurment_ReturnBadRequest()
        {
            //Act
            var actionResult = (ObjectResult)await _userBookController.AddUserBookAsync(null);

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.BadRequest, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetBooksAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var userId = 1;

            //Act
            var actionResult = await _userBookController.GetBooksAsync(userId);

            //Assert
            _mockUserBookAppService.Verify(a => a.GetBooksByUserIdAsync(userId), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);                  
        }

        [TestMethod]
        public async Task GetBooksAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            var userId = 1;
            _mockUserBookAppService.Setup(a => a.GetBooksByUserIdAsync(userId)).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _userBookController.GetBooksAsync(userId);

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetBooksCompletedAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var userId = 1;

            //Act
            var actionResult = await _userBookController.GetBooksCompletedAsync(userId);

            //Assert
            _mockUserBookAppService.Verify(a => a.GetBooksCompletedAsync(userId), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);
        }

        [TestMethod]
        public async Task GetBooksCompletedAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            var userId = 1;
            _mockUserBookAppService.Setup(a => a.GetBooksCompletedAsync(userId)).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _userBookController.GetBooksCompletedAsync(userId);

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }        
    }
}
