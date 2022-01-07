CREATE PROCEDURE [dbo].[fmUser_UpdateUserInfo]
	@Username nvarchar(50),
	@Email nvarchar(50),
	@Id int
AS
BEGIN

	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		UPDATE dbo.[User] SET dbo.[User].UserName = @Username, dbo.[User].Email = @Email
		WHERE dbo.[User].Id = @Id
END
