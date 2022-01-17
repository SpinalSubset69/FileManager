CREATE PROCEDURE [dbo].[fmFile_GetFileByName]
	@FileName varchar(255)
AS
BEGIN
	IF NOT EXISTS (SElECT 1 FROM dbo.[File] WHERE dbo.[File].FileName = @FileName)
		RAISERROR('File Not Found On Database', 16, 1)
	ELSE
	SElECT * FROM dbo.[File] WHERE dbo.[File].FileName = @FileName
END
