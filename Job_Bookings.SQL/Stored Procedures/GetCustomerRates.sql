CREATE PROCEDURE [dbo].[GetCustomerRates]
	@guid UNIQUEIDENTIFIER
AS
BEGIN

	DECLARE @customerId bigint 
	exec @customerId = dbo.[GetCustomerId] @CustomerGuid = @guid

	SELECT 
		CASE WHEN cr.dateupdated IS NULl THEN 1 ELSE 0 END AS 'IsActive',
		cr.rateGuid,
		@guid as 'customerGuid',
		cr.hourlyrate,
		cr.datecreated,
		cr.dateupdated
		FROM 
		customerrates AS cr
		WHERE cr.customerId = @customerId
		ORDER BY cr.datecreated DESC
		FOR JSON PATH
		RETURN 
END