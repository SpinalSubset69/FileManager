CREATE PROCEDURE [dbo].[fmUser_GetFiles]
	@Id int
AS

BEGIN
	IF NOT EXISTS (SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1)
	ELSE
		SELECT * FROM dbo.[File] 
		WHERE dbo.[File].UserId = @Id 
END
