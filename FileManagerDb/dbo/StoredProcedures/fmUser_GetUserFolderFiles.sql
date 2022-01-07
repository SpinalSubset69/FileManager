CREATE PROCEDURE [dbo].[fmUser_GetUserFolderFiles]
	@UserId int,
	@FolderId int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Id = @FolderId)
		RAISERROR('Folder Not Found on Database', 16, 1)
	ELSE
		IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @UserId)
			RAISERROR('User Not Found on Database', 16, 1)
		ELSE
			SELECT * FROM dbo.[File] WHERE dbo.[File].UserId = @UserId AND dbo.[File].FolderId = @FolderId
END
