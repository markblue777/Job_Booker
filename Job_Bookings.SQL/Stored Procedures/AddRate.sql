CREATE PROCEDURE [dbo].[AddRate]
	@json NVARCHAR(MAX)
AS
	INSERT INTO customerrates
	(
		customerId,
		hourlyrate,
		rateGuid,
		datecreated,
		dateupdated
	)
	SELECT
		[dbo].[GetCustomerId](customerGuid),
		hourlyRate,
		rateGuid,
		dateCreated,
		dateUpdated
	FROM 
	OPENJSON(@json)
	WITH
	(
		rateGuid UNIQUEIDENTIFIER,
		customerGuid UNIQUEIDENTIFIER,
		hourlyRate FLOAT,
		dateCreated DATETIME,
		dateUpdated DATETIME
	)
RETURN