CREATE TABLE [dbo].[expenses]
(
	[expenseTypeId] BIGINT NOT NULL PRIMARY KEY, 
    [expenseGuid] UNIQUEIDENTIFIER NOT NULL,
    [receiptId] NCHAR(10) NULL, 
    [description] NVARCHAR(MAX) NULL,
    [amount] MONEY NOT NULL,
    [userId] BIGINT NOT NULL, 
    [customerId] BIGINT NULL, 
    [appointmentId] BIGINT NULL,
    [datecreated] DATETIME NOT NULL, 
    [dateupdated] DATETIME NULL, 
    [archived] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [UK_UserId_receiptId] UNIQUE ([receiptId], [userId])
)
