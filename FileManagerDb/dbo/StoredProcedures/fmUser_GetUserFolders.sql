CREATE PROCEDURE [dbo].[fmUser_GetUserFolders]
	@UserId int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @UserId)
		RAISERROR('User Not Found on DataBase', 16, 1);
	ELSE
		SELECT * FROM dbo.[Folder] WHERE dbo.[Folder].UserId = @UserId
END
