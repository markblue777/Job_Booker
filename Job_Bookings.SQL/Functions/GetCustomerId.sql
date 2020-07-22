CREATE Function [dbo].[GetCustomerId](@CustomerGuid UNIQUEIDENTIFIER)
RETURNS int
AS
BEGIN 
	DECLARE @CustomerId int
	SELECT @CustomerId= customerId FROM customer where customerGuid = @CustomerGuid
	
	RETURN @CustomerId
END