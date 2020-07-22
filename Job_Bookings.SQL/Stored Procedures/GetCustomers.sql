CREATE PROCEDURE [dbo].[GetCustomers]
	@userGuid uniqueidentifier
AS
BEGIN 
		DECLARE @userId bigint

		select @userId = userId from users where userGuid = @userGuid

		SELECT
		(
			SELECT 
			c.customerGuid,
			@UserGuid as 'UserGuid',
			c.firstname,
			c.lastname,
			c.phonenumber,
			c.mobilenumber,
			c.email,
			c.addressline1,
			c.city,
			c.county,
			c.postcode,
			c.milesfromhomebase,
			c.archived,
			c.datecreated,
			c.dateupdated,
			(
				SELECT 
				CASE WHEN cr.dateupdated IS NULl THEN 1 ELSE 0 END AS 'IsActive',
				cr.rateGuid,
				c.customerGuid,
				cr.hourlyrate,
				cr.datecreated,
				cr.dateupdated
				FROM 
				customerrates AS cr
				WHERE cr.customerId = c.customerId
				ORDER BY cr.datecreated DESC
				FOR JSON PATH
			) as [Rates]
			
			FROM 
			customer as c
			where userId = @UserId
			FOR JSON PATH
		) as Customers
		FOR JSON PATH, WITHOUT_ARRAY_WRAPPER;
		
		RETURN
END