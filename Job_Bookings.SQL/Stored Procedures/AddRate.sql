CREATE PROCEDURE [dbo].[AddRate]
	@json NVARCHAR(MAX)
AS
	--store the json in a temp table to work with
	SELECT
		[dbo].[GetCustomerId](customerGuid) as 'CustomerId',
		hourlyRate,
		rateGuid,
		dateCreated,
		dateUpdated
	INTO #TempCustomerRate
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

	--get the customer the rate is for
	DECLARE @customerId BIGINT;
	
	SELECT TOP 1 @customerId = CustomerId FROM #TempCustomerRate ORDER BY dateCreated DESC

	--update a customers old active rate 
	
	UPDATE customerrates SET dateupdated = (select datecreated from #TempCustomerRate)  where customerId = @customerId AND dateupdated IS NULL


	--insert the customers new rate

	INSERT INTO customerrates
	(
		customerId,
		hourlyrate,
		rateGuid,
		datecreated,
		dateupdated
	)
	SELECT
		CustomerId,
		hourlyRate,
		rateGuid,
		dateCreated,
		dateUpdated
	FROM 
	#TempCustomerRate

	DROP TABLE #TempCustomerRate

RETURN