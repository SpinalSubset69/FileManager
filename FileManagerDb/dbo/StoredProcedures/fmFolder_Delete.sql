CREATE PROCEDURE [dbo].[fmFolder_Delete]
	@Id int	
AS
BEGIN
IF NOT EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Id = @Id)
		RAISERROR('Folder Not Found on DataBase', 16, 1);
	ELSE
		DELETE FROM dbo.[Folder] WHERE dbo.[Folder].Id = @Id
END
