CREATE PROCEDURE [dbo].[fmFile_Delete]
	@Id int	
AS
BEGIN
IF NOT EXISTS(SELECT 1 FROM dbo.[File] WHERE dbo.[File].Id = @Id)
		RAISERROR('File Not Found on DataBase', 16, 1);
	ELSE
		DELETE FROM dbo.[File]
		WHERE dbo.[File].Id =	@Id
END
