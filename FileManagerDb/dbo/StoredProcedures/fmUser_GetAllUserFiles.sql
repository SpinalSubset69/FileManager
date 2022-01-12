CREATE PROCEDURE [dbo].[fmUser_GetAllUserFiles]
	@Id int	
AS
		IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
		RAISERROR('User Not Found on Database', 16, 1);
	ELSE
		SELECT *
		FROM dbo.[File] WHERE dbo.[File].UserId = @Id
RETURN 0
