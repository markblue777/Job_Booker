CREATE PROCEDURE [dbo].[AddUser]
	@json NVARCHAR(MAX)
AS
	INSERT INTO [dbo].[Users] (
		username,
		email,
		addressline1,
		city,
		county,
		postcode,
		companyname,
		datecreated,
		dateupdated,
		userguid,
		timezone,
		firstname,
		lastname,
		archived
	)
	SELECT 
		Username,
		Email,
		AddressLine1,
		City,
		County,
		Postcode,
		CompanyName,
		dateCreated,
		dateUpdated,
		UserGuid,
		Timezone,
		FirstName,
		LastName,
		Archived
	FROM 
	OPENJSON(@JSON)
	WITH(
	    UserGuid UNIQUEIDENTIFIER,
        Username NVARCHAR(50),
        Email NVARCHAR(80),
        AddressLine1 NVARCHAR(200),        
        City NVARCHAR(50),
        County NVARCHAR(50),
        Postcode NVARCHAR(20),
        CompanyName NVARCHAR(255),
        Timezone NVARCHAR(255),
        FirstName NVARCHAR(50),
        LastName NVARCHAR(50),
        Archived BIT,
		dateCreated DATETIME,
		dateUpdated DATETIME
	)
RETURN
