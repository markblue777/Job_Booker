using Job_Bookings.Models;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class CustomerRatesRepo : RepoBase<CustomerRatesRepo>, ICustomerRatesRepo
    {
        public CustomerRatesRepo(IConfiguration confg, ILogger<CustomerRatesRepo> logger, IRetryPolicy retryPolicy):base(confg,logger, retryPolicy)
        {

        }

        public async Task<bool> AddCustomerRate(CustomerRate customerRate)
        {
            List<SqlParameter> sqlParams = new List<SqlParameter>();
            sqlParams.Add(new SqlParameter { ParameterName = "@json", Value = JsonConvert.SerializeObject(customerRate) });

            return await ExecuteWriterAsync("dbo.AddRate", sqlParams);
        }
    }
}
