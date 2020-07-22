CREATE Function [dbo].[GetCustomerId](@CustomerGuid UNIQUEIDENTIFIER)
RETURNS BIGINT
AS
BEGIN 
	DECLARE @CustomerId int = -1
	SELECT @CustomerId= customerId FROM customer where customerGuid = @CustomerGuid
	
	RETURN @CustomerId
END