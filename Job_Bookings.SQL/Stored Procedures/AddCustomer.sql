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


{
  "customerGuid": "64D3052F-D653-4DA8-AE92-CB1C5C5C7C1A",
  "userGuid": "25AA7F5E-6C7C-48D3-88EB-ECC4AEE50210",
  "firstname": null,
  "lastname": null,
  "phonenumber": null,
  "mobilenumber": null,
  "email": null,
  "addressline1": null,
  "city": null,
  "county": null,
  "postcode": null,
  "milesfromhomebase": 0.0,
  "rates": [
	{
	  "rateGuid": "839E549D-74DC-4237-9E89-6C8955662D1B",
	  "isActive": false,
	  "customerGuid": "64D3052F-D653-4DA8-AE92-CB1C5C5C7C1A",
	  "hourlyRate": 12.0,
	  "dateCreated": "2020-06-07T00:00:00",
	  "dateUpdated": "2020-07-12T00:00:00"
	},
	{
	  "rateGuid": "4145E9B7-378E-4F0D-9D74-98A5BCAE2D70",
	  "isActive": false,
	  "customerGuid": "64D3052F-D653-4DA8-AE92-CB1C5C5C7C1A",
	  "hourlyRate": 15.0,
	  "dateCreated": "2020-07-12T00:00:00",
	  "dateUpdated": null
	},
  ],
  "archived": false,
  "dateCreated": "0001-01-01T00:00:00",
  "dateUpdated": "0001-01-01T00:00:00"
}
