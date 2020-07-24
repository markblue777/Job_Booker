using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Job_Bookings.Services
{
    public abstract class BaseService<T> where T : class
    {
        protected ILogger<T> _logger;
    }
}
