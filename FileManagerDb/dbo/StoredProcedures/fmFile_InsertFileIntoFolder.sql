CREATE PROCEDURE [dbo].[fmFile_InsertFileIntoFolder]
	@FolderId int,	
	@Id int,
	@UserID int
AS
BEGIN
If NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @UserID)
	RAISERROR('User Not Found on Database', 16, 1);
ELSE
	IF NOT EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Id = @FolderId AND dbo.[Folder].UserId = @UserID)
		RAISERROR('Folder Not Found on User Account', 16, 1);
	ELSE
		IF NOT EXISTS(SELECT 1 FROM dbo.[File] WHERE dbo.[File].Id = @Id)
			RAISERROR('File Not Found on User Account', 16, 1);
		ELSE
			UPDATE dbo.[File] SET dbo.[File].FolderId = @FolderId WHERE dbo.[File].Id = @Id;		
END
