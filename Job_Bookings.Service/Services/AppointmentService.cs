using Job_Bookings.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Job_Bookings.Services
{
    public class AppointmentService : BaseService<AppointmentService>, IAppointmentService
    {
        readonly IAppointmentRepo _appointmentRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo, ILogger<AppointmentService> logger)
        {
            _appointmentRepo = appointmentRepo;
            _logger = logger;
        }

        public async Task<ReturnDto<bool>> AddAppointment(Appointment app)
        {
            var rtn = new ReturnDto<bool>();

            if (app == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.ReturnObject = false;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _appointmentRepo.AddAppointment(app);
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = false;

                _logger.LogError($"An error occured in - {typeof(AppointmentService)} - Add Appointment - Message: {e.Message} - A: {app.AppointmentGuid}, U: {app.UserGuid}, C: {app.CustomerGuid}");
            }

            return rtn;
        }

        /// <summary>
        /// This sets a booking to cancelled. You do not want a hard delete as 
        /// it could be used in analytics as to what customers mess you around
        /// </summary>
        /// <param name="appGuid"></param>
        /// <returns></returns>
        public async Task<ReturnDto<bool>> DeleteAppointment(Guid appGuid)
        {
            var rtn = new ReturnDto<bool>();

            if (appGuid == null || appGuid == Guid.Empty)
            {
                rtn.ErrorCode = ErrorCodes.APPOINTMENT_GUID_NOT_PROVIDED;
                rtn.ReturnObject = false;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _appointmentRepo.DeleteAppointment(appGuid);
            }
            catch (Exception e) {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = false;

                _logger.LogError($"An error occured in - {typeof(AppointmentService)} - Delete Appointment - Message: {e.Message} - A: {appGuid}");

            }

            return rtn;
        }

        public async Task<ReturnDto<List<Appointment>>> GetAllAppointments(Guid userGuid)
        {
            var rtn = new ReturnDto<List<Appointment>>();

            if (userGuid == null || userGuid == Guid.Empty)
            {
                rtn.ErrorCode = ErrorCodes.USER_GUID_NOT_PROVIDED;
                rtn.ReturnObject = null;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _appointmentRepo.GetAppointments(userGuid: userGuid);
            } catch (Exception e) {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {typeof(AppointmentService)} - Get All Appointments - Message: {e.Message} - A: {userGuid}");

            }

            return rtn;
        }

        public async Task<ReturnDto<List<Appointment>>> GetAppointmentsByDate(DateTime dt, Guid userGuid, bool dayOnly = false)
        {
            var rtn = new ReturnDto<List<Appointment>>();

            if (userGuid == null || userGuid == Guid.Empty)
            {
                rtn.ErrorCode = ErrorCodes.USER_GUID_NOT_PROVIDED;
                rtn.ReturnObject = null;

                return rtn;
            }

            try
            {
                rtn.ReturnObject =  await _appointmentRepo.GetAppointments(userGuid, dt, dayOnly);
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {typeof(AppointmentService)} - Get Appointments By Date- Message: {e.Message} - DT: {dt}, U: {userGuid}, DO: {dayOnly}");

            }

            return rtn;
        }

        public async Task<ReturnDto<Appointment>> UpdateAppointment(Appointment app)
        {
            var rtn = new ReturnDto<Appointment>();

            if (app == null)
            {
                rtn.ErrorCode = ErrorCodes.OBJECT_NOT_PROVIDED;
                rtn.ReturnObject = null;

                return rtn;
            }

            try
            {
                rtn.ReturnObject = await _appointmentRepo.UpdateAppointment(app);
            }
            catch (Exception e)
            {
                rtn.ErrorCode = ErrorCodes.OTHER;
                rtn.ReturnObject = null;

                _logger.LogError($"An error occured in - {typeof(AppointmentService)} - Update Appointments - Message: {e.Message} - A: {app.AppointmentGuid}, U: {app.UserGuid}, C: {app.CustomerGuid}");

            }

            return rtn;
        }
    }
}
