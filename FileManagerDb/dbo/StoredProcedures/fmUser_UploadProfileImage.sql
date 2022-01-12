CREATE PROCEDURE [dbo].[fmUser_UploadProfileImage]
	@Id int,
	@ImageName varchar(255)
AS
BEGIN
		IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		INSERT INTO dbo.[User](dbo.[User].ProfileImage) VALUES (@ImageName);

END