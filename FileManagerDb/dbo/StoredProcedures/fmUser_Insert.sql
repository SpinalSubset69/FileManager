CREATE PROCEDURE [dbo].[fmUser_Insert]
	@UserName nvarchar(50),
	@Email nvarchar(50),
	@Password VARBINARY(MAX),
	@PasswordSalt VARBINARY(MAX),
	@Created_At datetime
AS
BEGIN 

	--Verify theres no other user with same email
	If EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Email = @Email)	
		RAISERROR('Email Already Registered on Database', 16, 1);	
	ELSE
	--Verify UserName its not taken
		IF EXISTS (SELECT 1 FROM dbo.[User] WHERE dbo.[User].UserName = @UserName)
			RAISERROR('User Already Registered on Database', 16, 1);
		ELSE
			INSERT INTO dbo.[User](UserName, Email, Password, PasswordSalt, Created_At)
			VALUES (@UserName, @Email, @Password, @PasswordSalt, @Created_At);
END