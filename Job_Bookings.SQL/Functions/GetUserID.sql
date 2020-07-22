CREATE Function [dbo].[GetUserId](@UserGuid UNIQUEIDENTIFIER)
RETURNS int
AS
BEGIN 
	DECLARE @UserId int
	SELECT @UserId= userId FROM users where userGuid = @UserGuid
	
	RETURN @UserId
END