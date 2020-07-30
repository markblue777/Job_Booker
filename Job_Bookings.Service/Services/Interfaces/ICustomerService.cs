using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface ICustomerService
    {

        Task<ReturnDto<List<Customer>>> ListCustomers(Guid userGuid, bool onlyActive = true);
        Task<ReturnDto<Customer>> GetCustomer(Guid userGuid, Guid customerGuid);
        Task<ReturnDto<bool>> AddCustomer(Customer cust);
        Task<ReturnDto<Customer>> UpdateCustomer(Customer cust);
    }
}
