CREATE TABLE [dbo].[paymenttype](
	[paymentTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[paymentTypeGuid] [uniqueidentifier] NOT NULL,
	[datecreated] DATETIME NULL, 
    [dateupdated] DATETIME NULL, 
    PRIMARY KEY CLUSTERED 
(
	[paymentTypeId] ASC
)
) ON [PRIMARY]
GO

--TODO: implement foreign key constraints where appropriate