using Castle.Core.Logging;
using Job_Bookings.Models;
using Job_Bookings.Models.Helper;
using Job_Bookings.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Tests
{
    public class UserServiceTests
    {
        readonly Mock<IUserRepo> _userRepoMock = new Mock<IUserRepo>();
        readonly Mock<ILogger<UserService>> _logger = new Mock<ILogger<UserService>>();

        readonly UserService _userService;

        List<User> _users = new List<User>();

        public UserServiceTests()
        {
            _userService = new UserService(_userRepoMock.Object, _logger.Object);
        }

        [SetUp]
        public void Setup()
        {
            _users.Add(new User { UserGuid= Guid.NewGuid() });

            _users.Add(new User { UserGuid = Guid.NewGuid() });

            _users.Add(new User { UserGuid = Guid.NewGuid() });
        }

        [Test]
        public async Task UserService_Add() {
            
            //Arrange
            var user = new User
            {
                UserGuid = Guid.NewGuid()
            };
            _userRepoMock.Setup(x => x.AddUser(user)).Callback((User user) => { _users.Add(user); }).ReturnsAsync(true);

            //Act
            var res = await _userService.AddUser(user);

            //Assert
            Assert.IsTrue(res.ReturnObject);
        }

        [Test]
        public async Task UserService_Add_Null_Object_Type() {

            //Arrange
            _userRepoMock.Setup(x => x.AddUser(It.IsAny<User>())).ReturnsAsync(true);

            //Act
            var res = await _userService.AddUser(null);

            //Assert
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED.GetDescription(), res.Message);

        }

        [Test]
        public async Task UserService_Add_Repo_Exception() {
            //Arrange
            var user = new User();
            _userRepoMock.Setup(x => x.AddUser(user)).ThrowsAsync(new Exception());

            //Act
            var res = await _userService.AddUser(user);

            //Assert
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _userRepoMock.Verify(x => x.AddUser(user), Times.Once);
        }

        [Test]
        public async Task UserService_GetUser() {
            //Arrange
            var user = new User {
                UserGuid = Guid.NewGuid()
            };

            _userRepoMock.Setup(x => x.GetUser(It.IsAny<Guid>())).ReturnsAsync(user);

            //Act
            var res = await _userService.GetUser(Guid.NewGuid());

            //Assert
            Assert.IsNotNull(res.ReturnObject);
            _userRepoMock.Verify(x => x.GetUser(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public async Task UserService_GetUser_Null_Object_Type()
        {
            //Arrange
            var guid = Guid.NewGuid();
            var user = new User
            {
                UserGuid = Guid.NewGuid()
            };

            _userRepoMock.Setup(x => x.GetUser(guid)).ReturnsAsync(user);

            //Act
            var res = await _userService.GetUser(Guid.Empty);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED.GetDescription(), res.Message);
            _userRepoMock.Verify(x => x.GetUser(guid), Times.Never);
        }

        [Test]
        public async Task UserService_GetUser_Repo_Exception()
        {
            //Arrange
            var guid = Guid.NewGuid();
            _userRepoMock.Setup(x => x.GetUser(guid)).ThrowsAsync(new Exception());

            //Act
            var res = await _userService.GetUser(guid);

            //Assert
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _userRepoMock.Verify(x => x.GetUser(guid), Times.Once);
        }
        
        [Test]
        public async Task UserService_GetUsers_Repo_Exception()
        {
            //Arrange
            _userRepoMock.Setup(x => x.GetUsers()).ThrowsAsync(new Exception());

            //Act
            var res = await _userService.GetUsers();

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _userRepoMock.Verify(x => x.GetUsers());
        }

        [Test]
        public async Task UserService_GetUsers() {
            //Arrange
            _userRepoMock.Setup(x => x.GetUsers()).ReturnsAsync(new List<User> { new User { UserGuid = Guid.NewGuid() }, new User { UserGuid = Guid.NewGuid() } });

            //Act
            var res = await _userService.GetUsers();

            //Assert
            Assert.AreEqual(2, res.ReturnObject.Count);
            _userRepoMock.Verify(x => x.GetUsers(), Times.Once);
        }

       

        [Test]
        public async Task UserService_UpdateUser() {
            _userRepoMock.Setup(x => x.UpdateUser(It.IsAny<User>())).ReturnsAsync(new User { UserGuid = Guid.NewGuid() });

            //Act
            var res = await _userService.UpdateUser(new User());

            //Assert
            Assert.IsNotNull(res.ReturnObject);
            _userRepoMock.Verify(x => x.UpdateUser(It.IsAny<User>()), Times.Once);
        }


        [Test]
        public async Task UserService_UpdateUser_Null_Object_Type()
        {
            //Assert
            var user = new User();
            _userRepoMock.Setup(x => x.UpdateUser(user)).ReturnsAsync(new User());

            //Act
            var res = await _userService.UpdateUser(null);

            //Assert
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED.GetDescription(), res.Message);
            _userRepoMock.Verify(x => x.UpdateUser(user), Times.Never);
        }


        [Test]
        public async Task UserService_UpdateUser_Repo_Exception()
        {
            var user = new User();
            _userRepoMock.Setup(x => x.UpdateUser(user)).ThrowsAsync(new Exception());

            //Act
            var res = await _userService.UpdateUser(user);

            //Assert
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _userRepoMock.Verify(x => x.UpdateUser(user), Times.Once);
        }


        [Test]
        public void User_Model_Check()
        {
            var t = typeof(User);

            Assert.AreEqual(14, t.GetProperties().Count());

        }
    }
}
