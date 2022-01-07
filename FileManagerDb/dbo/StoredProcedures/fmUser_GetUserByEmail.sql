CREATE PROCEDURE [dbo].[fmUser_GetUserByEmail]
	@email nvarchar(50)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Email = @Email)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		SELECT dbo.[User].Id, dbo.[User].UserName, dbo.[User].ProfileImage, dbo.[User].Email, dbo.[User].Password, dbo.[User].PasswordSalt, dbo.[User].Created_At
		FROM dbo.[User] WHERE dbo.[User].Email = @Email
END

