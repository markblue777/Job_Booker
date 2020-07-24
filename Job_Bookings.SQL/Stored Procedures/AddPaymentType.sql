CREATE PROCEDURE [dbo].[AddPaymentType]
	@Name NVARCHAR(50),
	@PaymentTypeGuid UNIQUEIDENTIFIER
AS
	INSERT INTO paymenttype ([name], [paymentTypeGuid], dateCreated)
	VALUES(@Name, @PaymentTypeGuid, GETUTCDATE())

RETURN
