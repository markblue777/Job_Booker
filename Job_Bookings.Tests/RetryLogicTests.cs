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

    public class RetryLogicTests
    {
        readonly Mock<IConfiguration> _configurationMock = new Mock<IConfiguration>();
        //Mock<IRetryPolicy> _retryPolicyMock = new Mock<IRetryPolicy>();
        readonly Mock<ILogger<RetryPolicy>> _logger = new Mock<ILogger<RetryPolicy>>();

        private readonly Mock<Func<Guid, Task<Customer>>> _repo = new Mock<Func<Guid, Task<Customer>>>();

        readonly IRetryPolicy _retryPolicy;

        readonly int retryAttemps = 3;
        readonly int retryDelay = 10;

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
            //Arrange
            Customer cust = new Customer();
            Guid customerGuid = Guid.NewGuid();
            _repo.Setup(p => p(It.IsAny<Guid>())).ReturnsAsync(cust);

            //Act
            var res = await _retryPolicy.Do(() => { return _repo.Object(customerGuid); });

            //Assert

            Assert.IsNotNull(res);
        }

        [Test]
        public async Task RetryLogic_SuccessAfterNRetry_Test()
        {
            //Arrange
            Customer cust = new Customer();
            Guid customerGuid = Guid.NewGuid();            
            _repo.SetupSequence(p => p(It.IsAny<Guid>())).ThrowsAsync(new Exception()).ThrowsAsync(new Exception()).ReturnsAsync(cust);


            //Act
            var res = await _retryPolicy.Do(() => { return _repo.Object(customerGuid); });

            //Assert
            Assert.IsNotNull(res);
            _repo.Verify(x => x(customerGuid), Times.Exactly(3)); 


        }

        [Test]
        public async Task RetryLogic_FailureAfterAllRetries_Test()
        {
            //Arrange
            Guid customerGuid = Guid.NewGuid();
            Guid userGuid = Guid.NewGuid();
            _repo.Setup(p => p(It.IsAny<Guid>())).ThrowsAsync(new Exception());

            //Act
            var res = await _retryPolicy.Do(() => { return _repo.Object(customerGuid);});

            //Assert
            Assert.IsNull(res);
            
            //it does the initial call, then 3 retries
            _repo.Verify(x => x(customerGuid), Times.Exactly(retryAttemps + 1));
        }

    }
}
