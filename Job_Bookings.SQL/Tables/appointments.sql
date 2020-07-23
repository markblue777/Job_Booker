CREATE TABLE [dbo].[appointments](
	[appointmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[appointmentDateTime] [datetime] NULL,
	[appointmentLength] [float] NULL,
	[customerId] [bigint] NULL,
	[userId] [bigint] NULL,
	[notes] [text] NULL,
	[materialCosts] [money] NULL,
	[additionalCosts] [money] NULL,
	[paymentTypeId] [bigint] NULL,
	[bookingCancelled] [bit] NULL,
	[dateCreated] [datetime] NULL,
	[dateUpdated] [datetime] NULL,
	[appointmentGuid] [uniqueidentifier] NOT NULL,
	[ratesId] [bigint] NOT NULL,
	[expectedTotal] [money] NULL,
PRIMARY KEY CLUSTERED 
(
	[appointmentId] ASC
)
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

--TODO: implement foreign key constraints where appropriate