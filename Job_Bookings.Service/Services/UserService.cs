using Microsoft.Extensions.Logging;

namespace Job_Bookings.Services
{
    public class UserService : BaseService<UserService>, IUserService
    {
        IUserRepo _userRepo;

        public UserService(IUserRepo userRepo, ILogger<UserService> logger)
        {
            _userRepo = userRepo;
            _logger = logger;
        }
    }
}
