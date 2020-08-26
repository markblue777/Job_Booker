CREATE TABLE [dbo].[appointmentTypes]
(
	[appointmentTypesId] BIGINT NOT NULL PRIMARY KEY, 
    [appointmentTypeGuid] UNIQUEIDENTIFIER NOT NULL, 
    [userId] BIGINT NOT NULL, 
    [type] NVARCHAR(255) NOT NULL, 
    [description] NVARCHAR(MAX) NULL,
    [datecreated] DATETIME NOT NULL, 
    [dateupdated] DATETIME NULL
)
