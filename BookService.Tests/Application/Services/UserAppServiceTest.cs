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
    public class UserAppServiceTest
    {
        private readonly Mock<IUserRepository> _mockUserRepository;
        private readonly UserAppService _userAppService;

        public UserAppServiceTest()
        {
            _mockUserRepository = new Mock<IUserRepository>();

            _userAppService = new UserAppService(_mockUserRepository.Object);
        }        

        [TestMethod]
        public async Task GetAllAsync_ThrowException()
        {
            //Mock
            _mockUserRepository.Setup(x => x.GetAllAsync()).ThrowsAsync(new Exception());

            await Assert.ThrowsExceptionAsync<Exception>(() => _userAppService.GetAllAsync());
        }

        [TestMethod]
        public async Task GetAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            int id = 1;
            var user = Mock.Of<User>(x => x.Id == 1);

            //Mock
            _mockUserRepository.Setup(x => x.GetAsync(id)).ReturnsAsync(user);
            var actionResult = await _userAppService.GetAsync(id);

            //Assert
            Assert.IsNotNull(actionResult);
            Assert.AreEqual(actionResult.Id, user.Id);
        }

        [TestMethod]
        public async Task GetAsync_ThrowException()
        {
            //Arrange
            int id = 1;

            //Mock
            _mockUserRepository.Setup(x => x.GetAsync(id)).ThrowsAsync(new Exception());

            await Assert.ThrowsExceptionAsync<Exception>(() => _userAppService.GetAsync(id));
        }

        [TestMethod]
        public async Task GetAllAsync_WhenValidArgument_ReturnOkResult()
        {
            //Arrange
            var users = new List<User>
            {
                new User { Id = 1 }
            };

            //Mock
            _mockUserRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(users);
            var actionResult = await _userAppService.GetAllAsync();

            //Assert
            Assert.IsNotNull(actionResult);
        }                
    }
}


