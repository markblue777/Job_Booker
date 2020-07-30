using Job_Bookings.Models;
using Job_Bookings.Models.Helper;
using Job_Bookings.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Tests
{
    public class CustomerRatesServiceTests
    {

        readonly Mock<ICustomerRatesRepo> _customerRatesRepo = new Mock<ICustomerRatesRepo>();
        readonly Mock<ILogger<CustomerRatesService>> _loggerMock = new Mock<ILogger<CustomerRatesService>>();
        readonly ICustomerRatesService _customerRatesService;

        readonly List<Rate> _customerRates = new List<Rate>();

        Guid _custOneGuid, _custTwoGuid, _rateOneGuid, _rateTwoGuid;

        public CustomerRatesServiceTests()
        {
            _custOneGuid = Guid.NewGuid();
            _custTwoGuid = Guid.NewGuid();
            _rateOneGuid = Guid.NewGuid();
            _rateTwoGuid = Guid.NewGuid();

            _customerRatesService = new CustomerRatesService(_customerRatesRepo.Object, _loggerMock.Object);

        }

        [SetUp]
        public void Setup()
        {
            _customerRates.Clear();
            _customerRates.Add(new Rate { CustomerGuid = _custOneGuid, HourlyRate = 22M, RateGuid = _rateOneGuid, DateCreated = DateTime.UtcNow });
        }

        [Test]
        public async Task CustomerRate_Add_New_Rate_For_New_Customer_Test() 
        {
            //Arrange
            var custRate = new Rate { CustomerGuid = _custTwoGuid, HourlyRate = 34M, RateGuid = _rateTwoGuid, DateCreated = DateTime.UtcNow };
            _customerRatesRepo.Setup(c => c.AddCustomerRate(custRate)).Callback((Rate custRate) => {
                _customerRates.Add(custRate);
            }).ReturnsAsync(true);

            //Act
            var res = await _customerRatesService.AddCustomerRate(custRate);

            //Assert
            Assert.IsTrue(res.ReturnObject);
            Assert.AreEqual(2, _customerRates.Count);
        }


        [Test]
        public async Task CustomerRate_Add_New_Rate_For_Existing_Customer_Test()
        {
            //Arrange
            var custRate = new Rate { CustomerGuid = _custOneGuid, HourlyRate = 34M, RateGuid = _rateTwoGuid, DateCreated = DateTime.UtcNow };
            _customerRatesRepo.Setup(c => c.AddCustomerRate(custRate)).Callback((Rate custRate) => {
                _customerRates.Where(x => x.CustomerGuid == custRate.CustomerGuid && x.DateUpdated == null).FirstOrDefault().DateUpdated = DateTime.UtcNow;
                _customerRates.Add(custRate);
            }).ReturnsAsync(true);

            //Act
            var res = await _customerRatesService.AddCustomerRate(custRate);

            //Assert
            Assert.IsTrue(res.ReturnObject);
            Assert.AreEqual(2, _customerRates.Count);
            Assert.NotNull(_customerRates.FirstOrDefault(x => x.RateGuid == _rateOneGuid).DateUpdated);
        }

        [Test]
        public async Task CustomerRate_No_Object_Pass_Test()
        {
            //Arrange
            Rate custRate = null;
            _customerRatesRepo.Setup(c => c.AddCustomerRate(custRate)).Callback((Rate custRate) => {
                _customerRates.Add(custRate);
            }).ReturnsAsync(true);

            //Act
            var res = await _customerRatesService.AddCustomerRate(custRate);

            //Assert
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED.GetDescription(), res.Message);
            Assert.IsFalse(res.ReturnObject);
            Assert.AreEqual(1, _customerRates.Count);
        }

        [Test]
        public async Task CustomerRate_Repo_Exception_Pass()
        {
            //Arrange
            Rate custRate = new Rate();
            _customerRatesRepo.Setup(c => c.AddCustomerRate(custRate)).ThrowsAsync(new Exception());

            //Act
            var res = await _customerRatesService.AddCustomerRate(custRate);

            //Assert
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            Assert.IsFalse(res.ReturnObject);
            Assert.AreEqual(1, _customerRates.Count);
        }
    }
}
