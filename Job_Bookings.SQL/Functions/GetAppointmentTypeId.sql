CREATE FUNCTION [dbo].[GetAppointmentTypeId]
(
	@guid UNIQUEIDENTIFIER
)
RETURNS INT
AS
BEGIN
	RETURN -1 --SELECT appointmentTypesId FROM [appointmentTypes] WHERE appointmentTypeGuid = '@guid'
END
