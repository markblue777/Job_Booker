using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface ICustomerRatesService
    {
        Task<ReturnDto<bool>> AddCustomerRate(Rate customerRate);
        Task<ReturnDto<List<Rate>>> GetCustomerRates(Guid customerGuid);
    }
}
