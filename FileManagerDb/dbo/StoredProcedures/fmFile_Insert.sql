CREATE PROCEDURE [dbo].[fmFile_Insert]
	@FileName varchar(50),
	@FileExtension varchar(50),
	@Created_At datetime,
	@UserId int,
	@FileSize varchar(MAX)
AS
BEGIN
	--Verify theres no other File with same name in User Records
	If EXISTS(SELECT 1 FROM dbo.[File] WHERE dbo.[File].FileName= @FileName AND dbo.[File].UserId = @UserId)	
		RAISERROR('File Already Registered on User Account', 16, 1);
	ELSE
		INSERT INTO dbo.[File](dbo.[File].FileName, dbo.[File].FileExtension, dbo.[File].Created_At, dbo.[file].FileSize, dbo.[file].UserId)
		VALUES(@FileName, @FileExtension, @Created_At, @FileSize, @UserId);
END
