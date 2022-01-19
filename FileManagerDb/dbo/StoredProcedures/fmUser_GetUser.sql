CREATE PROCEDURE [dbo].[fmUser_GetUser]
	@Id int
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		SELECT dbo.[User].Id, dbo.[User].UserName, dbo.[User].ProfileImage, dbo.[User].Email, dbo.[User].Password, dbo.[User].PasswordSalt, dbo.[User].Created_At, dbo.[User].SpaceInUse
		FROM dbo.[User] WHERE dbo.[User].Id = @Id
END

