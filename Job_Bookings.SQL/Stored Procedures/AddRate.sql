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



	  "rateGuid": "839E549D-74DC-4237-9E89-6C8955662D1B",
	  "isActive": false,
	  "customerGuid": "64D3052F-D653-4DA8-AE92-CB1C5C5C7C1A",
	  "hourlyRate": 12.0,
	  "dateCreated": "2020-06-07T00:00:00",
	  "dateUpdated": "2020-07-12T00:00:00"