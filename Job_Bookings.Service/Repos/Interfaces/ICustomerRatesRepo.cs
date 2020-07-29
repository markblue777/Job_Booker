using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface ICustomerRatesRepo
    {
        Task<bool> AddCustomerRate(Rate customerRate);
    }   
}
