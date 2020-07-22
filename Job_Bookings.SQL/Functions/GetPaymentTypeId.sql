CREATE FUNCTION [dbo].[GetPaymentTypeId](@PaymentTypeGuid UNIQUEIDENTIFIER)
RETURNS BIGINT
AS
BEGIN
	DECLARE @PaymentTypeId BIGINT = -1
	SELECT @PaymentTypeId = paymentTypeId FROM paymenttype WHERE paymentTypeGuid = @PaymentTypeGuid

	RETURN @PaymentTypeId
END
