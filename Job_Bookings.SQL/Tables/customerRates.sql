CREATE TABLE [dbo].[customerrates](
	[ratesId] [bigint] IDENTITY(1,1) NOT NULL,
	[customerId] [bigint] NOT NULL,
	[hourlyrate] [money] NULL,
	[datecreated] [datetime] NULL,
	[dateupdated] [datetime] NULL,
	[rateGuid] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ratesId] ASC
)
) ON [PRIMARY]
GO