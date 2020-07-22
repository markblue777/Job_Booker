CREATE Function [dbo].[GetUserId](@UserGuid UNIQUEIDENTIFIER)
RETURNS BIGINT
AS
BEGIN 
	DECLARE @UserId int = -1
	SELECT @UserId= userId FROM users where userGuid = @UserGuid
	
	RETURN @UserId
END