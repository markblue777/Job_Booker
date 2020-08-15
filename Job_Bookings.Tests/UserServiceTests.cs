using Castle.Core.Logging;
using Job_Bookings.Models;
using Job_Bookings.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
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

    }
}
