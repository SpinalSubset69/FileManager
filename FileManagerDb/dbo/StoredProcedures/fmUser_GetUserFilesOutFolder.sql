CREATE PROCEDURE [dbo].[fmUser_GetUserFilesOutFolder]
	@Id int
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1)
	ELSE
		SELECT * FROM dbo.[File] 
		WHERE dbo.[File].UserId = @Id AND dbo.[File].FolderId is null
END
