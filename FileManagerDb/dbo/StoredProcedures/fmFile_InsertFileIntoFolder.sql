CREATE PROCEDURE [dbo].[fmFile_InsertFileIntoFolder]
	@FolderId int,	
	@Id int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Id = @FolderId)
		RAISERROR('Folder Not Found on DataBase', 16, 1);
	ELSE
		UPDATE dbo.[File] SET dbo.[File].FolderId = @FolderId WHERE dbo.[File].Id = @Id;
END
