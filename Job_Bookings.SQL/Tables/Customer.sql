CREATE TABLE [dbo].[customer](
	[customerId] [bigint] IDENTITY(1,1) NOT NULL,
	[userId] [bigint] NOT NULL,
	[firstname] [nvarchar](50) NULL,
	[lastname] [nvarchar](50) NULL,
	[phonenumber] [nvarchar](20) NULL,
	[mobilenumber] [nvarchar](20) NULL,
	[email] [nvarchar](80) NULL,
	[addressline1] [nvarchar](200) NULL,
	[city] [nvarchar](50) NULL,
	[county] [nvarchar](50) NULL,
	[postcode] [nvarchar](10) NULL,
	[milesfromhomebase] [float] NULL,
	[datecreated] [datetime] NULL,
	[dateupdated] [datetime] NULL,
	[customerGuid] [uniqueidentifier] NOT NULL,
	[archived] [bit] NOT NULL DEFAULT 0,
PRIMARY KEY CLUSTERED 
(
	[customerId] ASC
)
) ON [PRIMARY]

--TODO: implement foreign key constraints where appropriate