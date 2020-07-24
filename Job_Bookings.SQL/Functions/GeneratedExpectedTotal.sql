CREATE FUNCTION [dbo].[GeneratedExpectedTotal]
(
	@appointmentLength FLOAT,
	@materialCost MONEY,
	@additionalCost MONEY,
	@rateGuid UNIQUEIDENTIFIER
)
RETURNS MONEY
AS
BEGIN
	DECLARE @expectedTotal MONEY = 0.0;
	DECLARE @subTotal MONEY = 0.0;
	DECLARE @rateId BIGINT;
	DECLARE @hourlyRate MONEY;
	
	exec @rateId = dbo.GetRateId @RateGuid = @rateGuid

	SELECT @hourlyRate = hourlyrate FROM customerrates where ratesId = @rateId

	--subcost =  time the hourly rate by appoinment length
	SET @subTotal = @hourlyRate * @appointmentLength;

	SET @expectedTotal = @subTotal + @additionalCost + @materialCost;

	RETURN @expectedTotal
END
