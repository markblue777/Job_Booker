using Job_Bookings.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public interface IAppointmentRepo
    {
        Task<bool> AddAppointment(Appointment app);

        Task<Appointment> UpdateAppointment(Appointment app);

        Task<bool> DeleteAppointment(Guid appGuid);

        Task<List<Appointment>> GetAppointments(Guid userGuid, DateTime? dateTime = null, bool dayOnly = false);
    }
}
