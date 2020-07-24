CREATE PROCEDURE [dbo].[AddCustomer]
	@json NVARCHAR(MAX)
AS
	INSERT INTO [dbo].[customer] (
		[userId],
		[firstname],
		[lastname],
		[phonenumber],
		[mobilenumber],
		[email],
		[addressline1],
		[city],
		[county],
		[postcode],
		[milesfromhomebase],
		[customerGuid],
		[archived],
		[datecreated],
		[dateupdated]
	)
	SELECT 
		[dbo].[GetUserId](UserGuid),
		firstname,
		lastname,
		phonenumber,
		mobilenumber,
		email,
		addressline1,
		city,
		county,
		postcode,
		milesfromhomebase,
		customerGuid,
		archived,
		dateCreated,
		dateUpdated
	FROM 
	OPENJSON(@JSON)
	WITH(
		UserGuid UNIQUEIDENTIFIER,
		firstname NVARCHAR(50),
		lastname NVARCHAR(50),
		phonenumber NVARCHAR(20),
		mobilenumber NVARCHAR(20),
		email NVARCHAR(80),
		addressline1 NVARCHAR(200),
		city NVARCHAR(50),
		county NVARCHAR(50),
		postcode NVARCHAR(10),
		milesfromhomebase FLOAT,
		customerGuid UNIQUEIDENTIFIER,
		archived BIT,
		dateCreated DATETIME,
		dateUpdated DATETIME
	)
RETURN
