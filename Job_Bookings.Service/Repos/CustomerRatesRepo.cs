using Job_Bookings.Models;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class CustomerRatesRepo : RepoBase<CustomerRatesRepo>, ICustomerRatesRepo
    {
        public CustomerRatesRepo(IConfiguration confg, ILogger<CustomerRatesRepo> logger, IRetryPolicy retryPolicy):base(confg,logger, retryPolicy)
        {

        }

        public Task<bool> AddCustomerRate(CustomerRate customerRate)
        {
            throw new NotImplementedException();
        }
    }
}
