CREATE PROCEDURE [dbo].[fmUser_UpdateUserPassword]
	@Password varbinary(MAX),
	@PasswordSalt varbinary(MAX),
	@Id int
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		UPDATE dbo.[User] SET dbo.[User].Password = @Password, dbo.[User].PasswordSalt = @PasswordSalt
		WHERE dbo.[User].Id = @Id
END
