CREATE FUNCTION [dbo].[GetAppointmentId](@AppointmentGuid UNIQUEIDENTIFIER)
RETURNS BIGINT
AS
BEGIN
	DECLARE @AppointmentId bigint = -1
	
	SELECT @AppointmentId = appointmentId FROM appointments where appointmentGuid = @AppointmentGuid
	
	RETURN @AppointmentId
END
