CREATE FUNCTION [dbo].[GetRateId](@RateGuid UNIQUEIDENTIFIER)
RETURNS BIGINT
AS
BEGIN
	DECLARE @RateId bigint = -1
	SELECT @RateId = ratesId FROM customerrates where rateGuid = @RateGuid
	
	RETURN @RateId
END
