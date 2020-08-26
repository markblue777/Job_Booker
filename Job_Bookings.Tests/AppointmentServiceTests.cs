using Job_Bookings.Models;
using Job_Bookings.Models.Helper;
using Job_Bookings.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Job_Bookings.Tests
{
    public class AppointmentServiceTests
    {
        private Mock<IAppointmentRepo> _appointmentMockRepo = new Mock<IAppointmentRepo>();
        readonly Mock<ILogger<AppointmentService>> _loggerMock = new Mock<ILogger<AppointmentService>>();
        private AppointmentService _appService;


        List<Appointment> _appointments = new List<Appointment>();

        Guid _userOne, _userTwo, _userThree, _userInvalid;
        Guid _appointmentOne, _appointmentTwo, _appointmentThree, _appointmentFour, _appointmentInvalid;
        Guid _rateOne, _rateTwo, _rateThree, _rateInvalid;

        public AppointmentServiceTests()
        {
            _appService = new AppointmentService(_appointmentMockRepo.Object, _loggerMock.Object);
            
            //User Guids
            _userOne = Guid.NewGuid();
            _userTwo = Guid.NewGuid();
            _userThree = Guid.NewGuid();
            _userInvalid = Guid.NewGuid();

            //Appointment Guids
            _appointmentOne = Guid.NewGuid();
            _appointmentTwo = Guid.NewGuid();
            _appointmentThree = Guid.NewGuid();
            _appointmentFour = Guid.NewGuid();
            _appointmentInvalid = Guid.NewGuid();

            //Rate Guids
            _rateOne = Guid.NewGuid();
            _rateTwo = Guid.NewGuid();
            _rateThree = Guid.NewGuid();
            _rateInvalid = Guid.NewGuid(); 

        }

        [SetUp]
        public void Setup()
        {
            _appointments.Clear();
            _appointments.Add(new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = _appointmentOne,
                AdditionalCosts = decimal.Parse("12.75"),
                AppointmentDateTime = new DateTime(2020, 7, 6, 10, 0, 0),
                AppointmentLength = 2.5,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("22"),
                Notes = "Additional charge for lawn seed spreading",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateOne,
                UserGuid = _userOne,
                ExpectedTotal = decimal.Parse("59.75")
            });

            _appointments.Add(new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = _appointmentTwo,
                AdditionalCosts = decimal.Parse("0"),
                AppointmentDateTime = new DateTime(2020, 7, 10, 10, 0, 0),
                AppointmentLength = 3,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("0"),
                Notes = "",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateTwo,
                UserGuid = _userOne,
                ExpectedTotal = decimal.Parse("30.00")
            });

            _appointments.Add(new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = _appointmentThree,
                AdditionalCosts = decimal.Parse("0"),
                AppointmentDateTime = new DateTime(2020, 7, 6, 14, 0, 0),
                AppointmentLength = 1,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("0"),
                Notes = "",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateTwo,
                UserGuid = _userOne,
                ExpectedTotal = decimal.Parse("30.00")
            });


            _appointments.Add(new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = Guid.NewGuid(),
                AdditionalCosts = decimal.Parse("0"),
                AppointmentDateTime = new DateTime(2020, 6, 6, 14, 0, 0),
                AppointmentLength = 1,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("0"),
                Notes = "",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateTwo,
                UserGuid = _userOne,
                ExpectedTotal = decimal.Parse("30.00")
            });

        }

        [Test]
        public async Task AppointmentService_Get_Appointments_All_Test()
        {
            //arrange
            Guid userGuid = _userOne;
            int expectedRowReturn = 4;
            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, null, false)).ReturnsAsync(_appointments.Where(a => a.UserGuid == userGuid).ToList());

            //Act
            var res = await _appService.GetAllAppointments(userGuid);

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(expectedRowReturn, res.ReturnObject.Count);
        }

        [Test]
        public async Task AppointmentService_Get_Appointments_All_Invalid_Guid_Test()
        {
            //arrange
            Guid userGuid = _userOne;            
            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, null, false)).ReturnsAsync(_appointments.Where(a => a.UserGuid == userGuid).ToList());

            //Act
            var res = await _appService.GetAllAppointments(Guid.Empty);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.GetAppointments(userGuid, null, false), Times.Never);
        }

        [Test]
        public async Task AppointmentService_Get_Appointments_All_Repo_Exception_Test()
        {
            //arrange
            Guid userGuid = _userOne;
            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, null, false)).ThrowsAsync(new Exception());

            //Act
            var res = await _appService.GetAllAppointments(userGuid);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.GetAppointments(userGuid, null, false), Times.Once);
        }


        [Test]
        public async Task AppointmentService_Get_Appointments_By_Date_For_A_Month_Test()
        {
            //arrange
            DateTime dt = new DateTime(2020, 7, 6);
            Guid userGuid = _userOne;
            int expectedRowReturn = 3;
            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, dt, false)).ReturnsAsync(_appointments.Where(a => a.UserGuid == userGuid && (a.AppointmentDateTime.Month == dt.Month && a.AppointmentDateTime.Year == dt.Year)).ToList());

            //Act
            var res = await _appService.GetAppointmentsByDate(dt, userGuid);

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(expectedRowReturn, res.ReturnObject.Count);
            Assert.IsTrue(res.ReturnObject.Where(a => a.AppointmentGuid == _appointmentOne || a.AppointmentGuid == _appointmentTwo || a.AppointmentGuid == _appointmentThree).ToList().Count == expectedRowReturn);
        }

        [Test]
        public async Task AppointmentService_Get_Appointments_By_Date_For_A_Month_Invalid_Guid_Test()
        {
            //arrange
            DateTime dt = new DateTime(2020, 7, 6);
            Guid userGuid = _userOne;
            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, dt, false)).ReturnsAsync(_appointments.Where(a => a.UserGuid == userGuid && (a.AppointmentDateTime.Month == dt.Month && a.AppointmentDateTime.Year == dt.Year)).ToList());

            //Act
            var res = await _appService.GetAppointmentsByDate(dt, Guid.Empty);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.GetAppointments(userGuid, dt, false), Times.Never);
        }

        [Test]
        public async Task AppointmentService_Get_Appointments_By_Date_For_A_Month_Repo_Expo_Test()
        {
            //arrange
            DateTime dt = new DateTime(2020, 7, 6);
            Guid userGuid = _userOne;
            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, dt, false)).ThrowsAsync(new Exception());

            //Act
            var res = await _appService.GetAppointmentsByDate(dt, userGuid);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.GetAppointments(userGuid, dt, false), Times.Once);
        }


        [Test]
        public async Task AppointmentService_Get_Appointments_By_Date_For_A_Day_Test()
        {
            //Arrange
            DateTime dt = new DateTime(2020, 7, 6);
            Guid userGuid = _userOne;
            int expectedRowReturn = 2;

            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, dt, true)).ReturnsAsync(_appointments.Where(a => a.UserGuid == userGuid && a.AppointmentDateTime.Date == dt.Date).ToList());

            //Act
            var res = await _appService.GetAppointmentsByDate(dt, userGuid, true);

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(expectedRowReturn, res.ReturnObject.Count);
            Assert.IsTrue(res.ReturnObject.Where(a => a.AppointmentGuid == _appointmentOne || a.AppointmentGuid == _appointmentThree).ToList().Count == expectedRowReturn);
        }

        [Test]
        public async Task AppointmentService_Get_Appointments_By_Date_For_A_Day_Invalid_Guid_Test()
        {
            //Arrange
            DateTime dt = new DateTime(2020, 7, 6);
            Guid userGuid = _userOne;
            

            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, dt, true)).ReturnsAsync(_appointments.Where(a => a.UserGuid == userGuid && a.AppointmentDateTime.Date == dt.Date).ToList());

            //Act
            var res = await _appService.GetAppointmentsByDate(dt, Guid.Empty, true);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.USER_GUID_NOT_PROVIDED.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.GetAppointments(userGuid, dt, true), Times.Never);
        }

        [Test]
        public async Task AppointmentService_Get_Appointments_By_Date_For_A_Day_Repo_Exception_Test()
        {
            //Arrange
            DateTime dt = new DateTime(2020, 7, 6);
            Guid userGuid = _userOne;

            _appointmentMockRepo.Setup(x => x.GetAppointments(userGuid, dt, true)).ThrowsAsync(new Exception());

            //Act
            var res = await _appService.GetAppointmentsByDate(dt, userGuid, true);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.GetAppointments(userGuid, dt, true), Times.Once);
        }

        [Test]
        public async Task AppointmentService_Add_Appointment_Test() 
        {
            Predicate<Appointment> predicate = delegate (Appointment a) { return (a.AppointmentGuid == _appointmentFour); };

            //Arrange
            var app = new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = _appointmentFour,
                AdditionalCosts = decimal.Parse("0"),
                AppointmentDateTime = new DateTime(2020, 6, 6, 14, 0, 0),
                AppointmentLength = 1,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("0"),
                Notes = "",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateThree,
                UserGuid = _userThree,
                ExpectedTotal = decimal.Parse("90.00")
            };

            _appointmentMockRepo.Setup(x => x.AddAppointment(app)).Callback((Appointment app) => { _appointments.Add(app); }).ReturnsAsync(true);


            //Act
            var res = await _appService.AddAppointment(app);
            
            //Assert
            Assert.IsNotNull(res);
            Assert.IsTrue(res.ReturnObject);
            Assert.IsTrue(_appointments.Exists(predicate));
        }

        [Test]
        public async Task AppointmentService_Add_Appointment_Null_Object_Test()
        {
            Predicate<Appointment> predicate = delegate (Appointment a) { return (a.AppointmentGuid == _appointmentFour); };

            //Arrange
            Appointment app = null;
            _appointmentMockRepo.Setup(x => x.AddAppointment(app)).Callback((Appointment app) => { _appointments.Add(app); }).ReturnsAsync(true);

            //Act
            var res = await _appService.AddAppointment(app);

            //Assert
            Assert.IsFalse(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED.GetDescription(), res.Message);
        }

        [Test]
        public async Task AppointmentService_Add_Appointment_Repo_Exception_Test()
        {
            Predicate<Appointment> predicate = delegate (Appointment a) { return (a.AppointmentGuid == _appointmentFour); };

            //Arrange
            Appointment app = new Appointment();
            _appointmentMockRepo.Setup(x => x.AddAppointment(app)).ThrowsAsync(new Exception());

            //Act
            var res = await _appService.AddAppointment(app);

            //Assert
            Assert.IsFalse(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.AddAppointment(app), Times.Once);
        }

        [Test]
        public async Task AppointmentService_Update_Appointment_Test()
        {
            //Arrange
            var appOriginal = new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = _appointmentFour,
                AdditionalCosts = decimal.Parse("0"),
                AppointmentDateTime = new DateTime(2020, 6, 6, 14, 0, 0),
                AppointmentLength = 1,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("0"),
                Notes = "",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateThree,
                UserGuid = _userThree,
                ExpectedTotal = decimal.Parse("90.00")
            };

            var appUpdate = new Appointment()
            {
                CustomerGuid = Guid.NewGuid(),
                AppointmentGuid = _appointmentFour,
                AdditionalCosts = decimal.Parse("20.75"),
                AppointmentDateTime = new DateTime(2020, 6, 6, 10, 0, 0),
                AppointmentLength = 3,
                BookingCancelled = false,
                DateCreated = DateTime.UtcNow,
                MaterialCosts = decimal.Parse("20.50"),
                Notes = "",
                PaymentTypeGuid = Guid.NewGuid(),
                RateGuid = _rateThree,
                UserGuid = _userThree,
                ExpectedTotal = decimal.Parse("131.25")
            };

            _appointments.Add(appOriginal);

            _appointmentMockRepo.Setup(x => x.UpdateAppointment(appUpdate)).Callback((Appointment app) => {
                var selectedAppIdx = _appointments.FindIndex(a => a == appOriginal);
                _appointments[selectedAppIdx] = appUpdate;
            
            }).ReturnsAsync(appUpdate);


            //Act
            var res = await _appService.UpdateAppointment(appUpdate);

            //Assert
            Assert.IsNotNull(res);
            Assert.AreEqual(appUpdate, res.ReturnObject);
            //Assert.IsTrue(_appointments.Exists(predicate));

            Assert.IsFalse(_appointments.FirstOrDefault(a => a == appUpdate).AdditionalCosts == appOriginal.AdditionalCosts );
            Assert.IsFalse(_appointments.FirstOrDefault(a => a == appUpdate).MaterialCosts == appOriginal.MaterialCosts);
            Assert.IsFalse(_appointments.FirstOrDefault(a => a == appUpdate).ExpectedTotal== appOriginal.ExpectedTotal);
        }

        [Test]
        public async Task AppointmentService_Update_Appointment_Null_Object_Test()
        {
            //Arrange
            _appointmentMockRepo.Setup(x => x.UpdateAppointment(It.IsAny<Appointment>())).ReturnsAsync(new Appointment());

            //Act
            var res = await _appService.UpdateAppointment(null);

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OBJECT_NOT_PROVIDED.GetDescription(), res.Message);
        }

        [Test]
        public async Task AppointmentService_Update_Appointment_Repo_Exception_Test()
        {
            //Arrange
            _appointmentMockRepo.Setup(x => x.UpdateAppointment(It.IsAny<Appointment>())).ThrowsAsync(new Exception());

            //Act
            var res = await _appService.UpdateAppointment(new Appointment());

            //Assert
            Assert.IsNull(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.UpdateAppointment(It.IsAny<Appointment>()), Times.Once);
        }

        [Test]
        public async Task AppointmentService_Delete_Appointment_Test()
        {
            //Arrange
            Guid appGuid = _appointmentOne;
            _appointmentMockRepo.Setup(x => x.DeleteAppointment(appGuid)).Callback((Guid guid) => {
                _appointments.Where(a => a.AppointmentGuid == guid).FirstOrDefault().BookingCancelled = true;
            }).ReturnsAsync(true);

            //Act
            var res = await _appService.DeleteAppointment(appGuid);

            //Assert
            Assert.IsTrue(res.ReturnObject);
            Assert.IsTrue(_appointments.Where(a => a.AppointmentGuid == appGuid).FirstOrDefault().BookingCancelled);
        }

        [Test]
        public async Task AppointmentService_Delete_Appointment_Invalid_Guid_Test()
        {
            //Arrange
            
            //Act
            var res = await _appService.DeleteAppointment(Guid.Empty);

            //Assert
            Assert.IsFalse(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.APPOINTMENT_GUID_NOT_PROVIDED, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.APPOINTMENT_GUID_NOT_PROVIDED.GetDescription(), res.Message);
        }

        [Test]
        public async Task AppointmentService_Delete_Appointment_Repo_Exception_Test()
        {
            //Arrange
            _appointmentMockRepo.Setup(x => x.DeleteAppointment(It.IsAny<Guid>())).ThrowsAsync(new Exception());
            //Act
            var res = await _appService.DeleteAppointment(Guid.NewGuid());

            //Assert
            Assert.IsFalse(res.ReturnObject);
            Assert.AreEqual(ErrorCodes.OTHER, res.ErrorCode);
            Assert.AreEqual(ErrorCodes.OTHER.GetDescription(), res.Message);
            _appointmentMockRepo.Verify(x => x.DeleteAppointment(It.IsAny<Guid>()), Times.Once);
        }

        [Test]
        public void AppointmentModel_PropertyNumber_Check() {
            var t = typeof(Appointment);

            Assert.AreEqual(15, t.GetProperties().Count());
        }
    }
}
