CREATE PROCEDURE [dbo].[AddAppointment]
	@json NVARCHAR(MAX)
AS
	INSERT INTO [dbo].[appointments]
	(
	[appointmentGuid],
	[appointmentDateTime],
	[appointmentLength],
	[customerId],
	[userId],
	[notes],
	[materialCosts],
	[additionalCosts],
	[paymentTypeId],
	[bookingCancelled],
	[ratesId],
	[expectedTotal],
	[dateCreated],
	[dateUpdated]	
	)
	SELECT 
		appointmentGuid,
		appointmentDateTime,
		appointmentLength,
		[dbo].[GetCustomerId](customerGuid),
		[dbo].[GetUserId] (userGuid),
		notes,
		materialCosts,
		additionalCosts,
		[dbo].[GetPaymentTypeId](paymentTypeGuid),
		bookingCancelled,
		[dbo].[GetRateId](rateGuid),
		[dbo].GeneratedExpectedTotal(AppointmentLength, MaterialCosts, AdditionalCosts, RateGuid),
		dateCreated,
		dateUpdated
	FROM OPENJSON(@json)
	WITH (
		AppointmentGuid UNIQUEIDENTIFIER,
		AppointmentDateTime DATETIME,
		AppointmentLength FLOAT,
		CustomerGuid UNIQUEIDENTIFIER,
		UserGuid UNIQUEIDENTIFIER,
		Notes NVARCHAR(MAX),
		MaterialCosts MONEY,
		AdditionalCosts MONEY,
		PaymentTypeGuid UNIQUEIDENTIFIER,
		BookingCancelled BIT,
		RateGuid UNIQUEIDENTIFIER,
		ExpectedTotal MONEY,
		DateCreated DATETIME,
		DateUpdated DATETIME
	)
RETURN