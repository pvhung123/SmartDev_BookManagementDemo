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
    public class UserControllerTest
    {
        private readonly Mock<IUserAppService> _mockUserAppService;
        private readonly UserController _userController;

        public UserControllerTest()
        {
            _mockUserAppService = new Mock<IUserAppService>();
            _userController = new UserController(_mockUserAppService.Object);
        }

        [TestMethod]
        public async Task GetAllAsync_WhenValidArgument_ReturnOkResult()
        {
            //Act
            var actionResult = await _userController.GetAllAsync();

            //Assert
            _mockUserAppService.Verify(a => a.GetAllAsync(), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);                  
        }

        [TestMethod]
        public async Task GetAllAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            _mockUserAppService.Setup(a => a.GetAllAsync()).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _userController.GetAllAsync();

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            int id = 1;
            //Act
            var actionResult = await _userController.GetAsync(id);

            //Assert
            _mockUserAppService.Verify(a => a.GetAsync(id), Times.Once);
            Assert.IsNotNull(actionResult);
            Assert.AreEqual((int)HttpStatusCode.OK, ((ObjectResult)actionResult).StatusCode);
        }

        [TestMethod]
        public async Task GetAsync_WhenExceptionThrowed_ReturnInternalServerError()
        {
            //Arrange
            _mockUserAppService.Setup(a => a.GetAsync(It.IsAny<int>())).Throws<NullReferenceException>();

            //Act
            var actionResult = (ObjectResult)await _userController.GetAsync(It.IsAny<int>());

            //Assert            
            Assert.AreEqual((int)HttpStatusCode.InternalServerError, actionResult.StatusCode);
        }
    }
}
