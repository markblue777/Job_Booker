CREATE TABLE [dbo].[users](
	[userId] [bigint] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NULL,
	[email] [nvarchar](80) NOT NULL,
	[addressline1] [nvarchar](200) NULL,
	[city] [nvarchar](50) NULL,
	[county] [nvarchar](50) NULL,
	[postcode] [nvarchar](20) NULL,
	[companyname] [nvarchar](255) NULL,
	[datecreated] [datetime] NOT NULL,
	[dateupdated] [datetime] NULL,
	[userGuid] [uniqueidentifier] NULL,
	[timezone] [nvarchar](255) NULL,
[firstname] NVARCHAR(50) NULL, 
    [lastname] NVARCHAR(50) NULL, 
    [archived] BIT NOT NULL DEFAULT 0, 
    PRIMARY KEY CLUSTERED 
(
	[userId] ASC
)
) ON [PRIMARY]
GO

--TODO: implement foreign key constraints where appropriate