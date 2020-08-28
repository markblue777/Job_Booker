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
), 
    CONSTRAINT [UC_CustomerRates_RatesGuid] UNIQUE ([rateGuid])
) ON [PRIMARY]
GO

--TODO: implement foreign key constraints where appropriate