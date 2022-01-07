CREATE PROCEDURE [dbo].[fmFolder_UpdateDescription]
	@Description nvarchar(255),
	@Id int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Id = @Id)
		RAISERROR('Folder Not Found on Database', 16, 1);
	ELSE
		UPDATE dbo.[Folder] SET dbo.[Folder].Description = @Description WHERE dbo.[Folder].Id = @Id; 
END
