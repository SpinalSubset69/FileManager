CREATE PROCEDURE [dbo].[fmUser_UpdateInsertUserImage]
	@ProfileImage varchar(MAX),	
	@Id int
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		UPDATE dbo.[User] SET dbo.[User].ProfileImage = @ProfileImage
		WHERE dbo.[User].Id = @Id
END
