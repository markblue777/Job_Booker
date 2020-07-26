using Microsoft.Extensions.Logging;

namespace Job_Bookings.Services
{
    public class CustomerRatesService : BaseService<CustomerRatesService>, ICustomerRatesService
    {
        ICustomerRatesRepo _customerRatesRepo;

        public CustomerRatesService(ICustomerRatesRepo customerRatesRepo, ILogger<CustomerRatesService> logger)
        {
            _customerRatesRepo = customerRatesRepo;
            _logger = logger;
        }
    }
}
