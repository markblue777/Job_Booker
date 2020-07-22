using Job_Bookings.Models;
using Job_Bookings.Services;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Job_Bookings.Tests
{

    class RetryLogicTests
    {
        Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        //Mock<IRetryPolicy> _retryPolicyMock = new Mock<IRetryPolicy>();
        Mock<ILogger<RetryPolicy>> _logger = new Mock<ILogger<RetryPolicy>>();

        Mock<ICustomerRepo> _custRepo = new Mock<ICustomerRepo>();
        IRetryPolicy _retryPolicy;

        int retryAttemps = 3;
        int retryDelay = 10;

        public RetryLogicTests()
        {
            _configurationMock.Setup(x => x["retryAmount"]).Returns(retryAttemps.ToString());
            _configurationMock.Setup(x => x["retryPause"]).Returns(retryDelay.ToString());
            //need to mock retryAmount values and retryPause
            _retryPolicy = new RetryPolicy(_configurationMock.Object, _logger.Object);
        }

        [SetUp]
        public void Setup() 
        { 
            

        }
              
        
        [Test]
        public async Task RetryLogic_Success_Test() 
        {
            ////Arrange
            //Customer cust = new Customer();
            //Guid customerGuid = Guid.NewGuid();
            //Guid userGuid = Guid.NewGuid();
            //_custRepo.Setup(x => x.GetCustomer(userGuid, customerGuid)).ReturnsAsync(cust);
            
            ////Func<Task<Customer>> remCust = () => { return _custRepo.Object.GetCustomer(userGuid, customerGuid); };
 
            ////Act
            //var res = await _retryPolicy.Do(() => { return _custRepo.Object.GetCustomer(userGuid, customerGuid); });

            ////Assert

            //Assert.IsNotNull(res);
        }

        [Test]
        public async Task RetryLogic_SuccessAfterNRetry_Test()
        {
            //Arrange
            Guid customerGuid = Guid.NewGuid();
            Guid userGuid = Guid.NewGuid();
            //do this but change return so it sends back sql exception and then a valid response
            //_custRepo.Setup(x => x.GetCustomer(userGuid, customerGuid)).ReturnsAsync(cust);
            //Act

            //Assert

        }

        [Test]
        public async Task RetryLogic_FailureAfterAllRetries_Test()
        {
            //Arrange
            Guid customerGuid = Guid.NewGuid();
            Guid userGuid = Guid.NewGuid();
            //do this but change return so it just sends back sql exceptions
            _custRepo.Setup(x => x.GetCustomer(userGuid, customerGuid)).ThrowsAsync(new Exception());
            //Act
            
            var res = await _retryPolicy.Do(() => { return _custRepo.Object.GetCustomer(userGuid, customerGuid); });

            //Assert

            Assert.IsNull(res);
            
            //it does the initial call, then 3 retries
            _custRepo.Verify(x => x.GetCustomer(userGuid, customerGuid), Times.Exactly(retryAttemps+1));
        }

    }
}
