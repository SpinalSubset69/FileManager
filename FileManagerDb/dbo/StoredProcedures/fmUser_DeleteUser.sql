CREATE PROCEDURE [dbo].[fmUser_DeleteUser]
	@Id int 
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		DELETE FROM dbo.[User] WHERE dbo.[User].Id = @Id
END
