using Job_Bookings.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class AppointmentService : IAppointmentService
    {
        IAppointmentRepo _appointmentRepo;
        ILogger<AppointmentService> _logger;
        public AppointmentService(IAppointmentRepo appointmentRepo, ILogger<AppointmentService> logger)
        {
            _appointmentRepo = appointmentRepo;
            _logger = logger;
        }

        public async Task<bool> AddAppointment(Appointment app)
        {
            return await _appointmentRepo.AddAppointment(app);
        }

        /// <summary>
        /// This sets a booking to cancelled. You do not want a hard delete as 
        /// it could be used in analytics as to what customers mess you around
        /// </summary>
        /// <param name="appGuid"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAppointment(Guid appGuid)
        {
            return await _appointmentRepo.DeleteAppointment(appGuid);
        }

        public async Task<List<Appointment>> GetAllAppointments(Guid userGuid)
        {
            return await _appointmentRepo.GetAppointments(userGuid: userGuid);
        }

        public async Task<List<Appointment>> GetAppointmentsByDate(DateTime dt, Guid userGuid, bool dayOnly = false)
        {
            return await _appointmentRepo.GetAppointments(userGuid, dt, dayOnly);
        }

        public async Task<Appointment> UpdateAppointment(Appointment app)
        {
            return await _appointmentRepo.UpdateAppointment(app);
        }
    }
}
