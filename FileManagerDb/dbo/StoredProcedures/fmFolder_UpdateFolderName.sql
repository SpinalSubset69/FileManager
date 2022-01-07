CREATE PROCEDURE [dbo].[fmFolder_UpdateFolderName]
	@Name nvarchar(50),
	@Id int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Id = @Id)
		RAISERROR('Folder Not Found on Database', 16, 1);
	ELSE
		UPDATE dbo.[Folder] SET dbo.[Folder].Name = @Name WHERE dbo.[Folder].Id = @Id; 
END
