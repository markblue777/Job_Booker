CREATE TABLE [dbo].[customrLogin]
(
	[customerLoginId] BIGINT NOT NULL PRIMARY KEY, 
    [passord] NVARCHAR(255) NULL, 
    [datecreated] DATETIME NOT NULL, 
    [dateupdated] NCHAR(10) NULL
)
