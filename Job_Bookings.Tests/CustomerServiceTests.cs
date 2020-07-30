using Job_Bookings.Models;
using Job_Bookings.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Tests
{
    public class CustomerServiceTests
    {

        readonly Mock<ICustomerRepo> _customerRepoMock = new Mock<ICustomerRepo>();
        readonly Mock<ILogger<CustomerService>> _loggerMock = new Mock<ILogger<CustomerService>>();
        readonly CustomerService _custService;

        readonly List<Customer> _customers = new List<Customer>();

        Guid _userOne, _userTwo, _userThree, _userInvalid;

        public CustomerServiceTests()
        {
            _custService = new CustomerService(_customerRepoMock.Object, _loggerMock.Object);
            _userOne = Guid.NewGuid();
            _userTwo = Guid.NewGuid();
            _userThree = Guid.NewGuid();
            _userInvalid = Guid.NewGuid();
        }

        [SetUp]
        public void Setup()
        {
            _customers.Clear();

             _customers.Add(new Customer() { FirstName = "Tim", LastName = "Blogs", UserGuid = _userOne, CustomerGuid = new Guid("E78383EE-7525-439D-8123-56D22EB6613A"), 
                AddressLine1 = "12 gold lane", 
                City = "London", 
                County="London", 
                DateCreated = DateTime.UtcNow, 
                Email="", 
                MilesFromHomeBase = 10, 
                MobileNumber = "01234567897", 
                PhoneNumber="01234567898", 
                PostCode = "es1 2el" });

            _customers.Add(new Customer() { FirstName = "Jimmy", LastName = "Short", UserGuid = _userTwo, CustomerGuid = new Guid("D07D42AC-B6A5-4A62-96B2-8629A08CDAE3"), 
                AddressLine1 = "31 tape it lane", 
                City = "Tring", 
                County="Essex", 
                DateCreated = DateTime.UtcNow, 
                Email="test@testemail.com", 
                MilesFromHomeBase = 22.7, 
                MobileNumber = "01234567898", 
                PhoneNumber="01234567899", 
                PostCode = "ex1 3kl" });

            _customers.Add(new Customer() { FirstName = "Timmy", LastName = "Long", UserGuid = _userTwo, CustomerGuid = new Guid("F294FC04-C4C5-4902-A998-05E84F520311"),
                AddressLine1 = "34 long lane",
                City = "waltham cross",
                County = "herts",
                DateCreated = DateTime.UtcNow,
                Email = "another@testemail.com",
                MilesFromHomeBase = 0.8,
                MobileNumber = "01234567899",
                PhoneNumber = "01234567890",
                PostCode = "en8 2pl" });
        }
        
        [Test]
        public async Task CustomerService_GetCustomerList_Test()
        {
            //Arrange
            var userGuid = _userTwo;
            var invalidUserId = _userInvalid;
            _customerRepoMock.Setup(x => x.ListCustomers(userGuid)).ReturnsAsync(_customers.Where(c => c.UserGuid == userGuid).ToList());

            //Act
            var customersrtn = await _custService.ListCustomers(userGuid);
            var custRtnerr = await _custService.ListCustomers(invalidUserId);


            //Assert
            Assert.IsTrue(customersrtn.ReturnObject.Count == 2);
            Assert.IsNull(custRtnerr.ReturnObject);
        }
        
        [Test]
        public async Task CustomerService_GetCustomer_Test() 
        {
            //Arrange
            var userGuid = _userTwo;
            var customerGuid = _customers[1].CustomerGuid;
            _customerRepoMock.Setup(x => x.GetCustomer(userGuid, customerGuid)).ReturnsAsync(_customers.Where(c => c.UserGuid == userGuid && c.CustomerGuid == customerGuid).FirstOrDefault);

            //Act
            var customer = await _custService.GetCustomer(userGuid, customerGuid);
            
            //Assert
            Assert.AreEqual("Jimmy", customer.ReturnObject.FirstName);
        }

        [Test]
        public async Task CustomerService_AddCustomer_Test() 
        {
            //Arrange            
            var cust = new Customer() { FirstName = "Joe", LastName = "Bland", UserGuid = _userThree, CustomerGuid = new Guid("00491186-735B-4B78-AB42-B7D86A88E0CF"), 
                AddressLine1 = "68 tilt st",
                City = "climb",
                County = "Turns",
                DateCreated = DateTime.UtcNow,
                Email = "another@testemail.com",
                MilesFromHomeBase = 20.8,
                MobileNumber = "01234567812",
                PhoneNumber = "01234567813",
                PostCode = "cl28 9zx" };

            var userGuid = _userThree;
            _customerRepoMock.Setup(x => x.AddCustomer(It.IsAny<Customer>())).Callback((Customer cust) => { _customers.Add(cust); } ).ReturnsAsync(true);
            _customerRepoMock.Setup(x => x.GetCustomer(userGuid, cust.CustomerGuid)).ReturnsAsync(_customers.Where(c => c.UserGuid == userGuid && c.CustomerGuid == cust.CustomerGuid).FirstOrDefault);
           
            //Act
            var res = await _custService.AddCustomer(cust);

            //Assert
            Assert.IsTrue(res.ReturnObject);
            _customerRepoMock.Verify(x => x.AddCustomer(It.IsAny<Customer>()), Times.Once);
            
            //Act
            var custRes = await _custService.GetCustomer(userGuid, cust.CustomerGuid);

            //Assert
            Assert.NotNull(cust);
            Assert.AreEqual("Joe", cust.FirstName);
            Assert.AreEqual("Bland", cust.LastName);

            //Clean Up
            _customers.Remove(cust);
        }

        [Test]
        public async Task CustomerService_UpdateCustomer_Test() 
        {
            //Arrange
            var custOriginal = new Customer()
            {
                FirstName = "Joe",
                LastName = "Bland",
                UserGuid = _userThree,
                CustomerGuid = new Guid("00491186-735B-4B78-AB42-B7D86A88E0CF"),
                AddressLine1 = "68 tilt st",
                City = "climb",
                County = "Turns",
                DateCreated = DateTime.UtcNow,
                Email = "another@testemail.com",
                MilesFromHomeBase = 20.8,
                MobileNumber = "01234567812",
                PhoneNumber = "01234567813",
                PostCode = "cl28 9zx"
            };

            var custUpdated = new Customer()
            {
                FirstName = "Joey",
                LastName = "Bland",
                UserGuid = _userThree,
                CustomerGuid = new Guid("00491186-735B-4B78-AB42-B7D86A88E0CF"),
                AddressLine1 = "68 tilt st",
                City = "climb",
                County = "Turns",
                DateCreated = DateTime.UtcNow,
                Email = "another@testemail.com",
                MilesFromHomeBase = 2.44,
                MobileNumber = "98765432101",
                PhoneNumber = "01234567813",
                PostCode = "cl28 9zx"
            };

            var userGuid = _userThree;

            _customers.Add(custOriginal);

            _customerRepoMock.Setup(x => x.UpdateCustomer(It.IsAny<Customer>())).Callback((Customer cust) =>
            {
                var customerIdx = _customers.FindIndex(c => c.UserGuid == cust.UserGuid && c.CustomerGuid == cust.CustomerGuid);
                _customers[customerIdx] = cust;
            }).ReturnsAsync(_customers.Where(c => c.UserGuid == custOriginal.UserGuid && c.CustomerGuid == custOriginal.CustomerGuid).FirstOrDefault);

            //Act
            var updatedCust = await _custService.UpdateCustomer(custUpdated);

            //Assert
            Assert.IsNotNull(updatedCust);
            Assert.AreEqual(custUpdated.FirstName, updatedCust.ReturnObject.FirstName);
            Assert.AreEqual(custUpdated.MobileNumber, updatedCust.ReturnObject.MobileNumber);
            Assert.AreEqual(custUpdated.MilesFromHomeBase, updatedCust.ReturnObject.MilesFromHomeBase);
            Assert.AreEqual(custUpdated.City, updatedCust.ReturnObject.City);

            //Clean up
            _customers.Remove(_customers.FirstOrDefault(c => c.UserGuid == userGuid && c.CustomerGuid == custUpdated.CustomerGuid));
        }
    }
}