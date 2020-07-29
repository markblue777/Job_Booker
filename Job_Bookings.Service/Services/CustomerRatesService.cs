using Job_Bookings.Models;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class CustomerRatesService : BaseService<CustomerRatesService>, ICustomerRatesService
    {
        readonly ICustomerRatesRepo _customerRatesRepo;

        public CustomerRatesService(ICustomerRatesRepo customerRatesRepo, ILogger<CustomerRatesService> logger)
        {
            _customerRatesRepo = customerRatesRepo;
            _logger = logger;
        }

        public async Task<ReturnDto<bool>> AddCustomerRate(Rate customerRate)
        {
            var rtn = new ReturnDto<bool>();

            if (customerRate == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.Message = "No customer rate was provided";
                rtn.ReturnObject = false;

                return rtn;
            }

            rtn.ReturnObject = await _customerRatesRepo.AddCustomerRate(customerRate);

            return rtn;
        }
    }
}
