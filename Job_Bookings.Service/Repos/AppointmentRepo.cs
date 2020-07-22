using Job_Bookings.Models;
using Job_Bookings.Services.Helper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class AppointmentRepo : RepoBase, IAppointmentRepo
    {
        public AppointmentRepo(IConfiguration config, ILogger<RepoBase> logger, IRetryPolicy retryPolicy) : base(config, logger, retryPolicy)
        {

        }

        public Task<bool> AddAppointment(Appointment app)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAppointment(Guid appGuid)
        {
            throw new NotImplementedException();
        }

        public Task<List<Appointment>> GetAppointments(Guid userGuid, DateTime? dateTime = null, bool dayOnly = false)
        {
            throw new NotImplementedException();
        }

        public Task<Appointment> UpdateAppointment(Appointment app)
        {
            throw new NotImplementedException();
        }
    }
}
