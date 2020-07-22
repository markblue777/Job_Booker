CREATE TABLE [dbo].[paymenttype](
	[paymentTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[paymentTypeGuid] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[paymentTypeId] ASC
)
) ON [PRIMARY]
GO