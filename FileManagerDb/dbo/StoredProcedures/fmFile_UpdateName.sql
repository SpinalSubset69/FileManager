CREATE PROCEDURE [dbo].[fmFile_UpdateName]
	@FileName varchar(50),
	@Id int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[File] WHERE dbo.[File].Id = @Id)
		RAISERROR('File Not Found on DataBase', 16, 1);
	ELSE
		UPDATE dbo.[File] SET dbo.[File].FileName= @FileName
		WHERE dbo.[File].Id = @Id
END
