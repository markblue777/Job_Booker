using Castle.Core.Logging;
using Job_Bookings.Services.Helper;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Tests
{
    
    class RetryLogicTests
    {
        Mock<IRetryPolicy> _retryPolicyMock = new Mock<IRetryPolicy>();


        [SetUp]
        public void Setup() 
        { 
            

        }

        [Test]
        public async Task RetryLogic_Success_Test() 
        {
            //Arrange

            //Act

            //Assert
        }

        [Test]
        public async Task RetryLogic_SuccessAfterNRetry_Test()
        {
            //Arrange

            //Act

            //Assert

        }

        [Test]
        public async Task RetryLogic_FailureAfterAllRetries_Test()
        {
            //Arrange

            //Act

            //Assert
        }

    }
}
