CREATE PROCEDURE [dbo].[fmFile_Get]
	@Id int	
AS
BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.[File] WHERE dbo.[File].Id = @Id)
		RAISERROR('File Not Found on Database', 16, 1)
	ELSE
		SELECT * FROM dbo.[File] WHERE dbo.[File].Id = @Id
END
