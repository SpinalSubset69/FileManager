CREATE PROCEDURE [dbo].[fmFolder_Insert]
	@Name nvarchar(50),
	@Created_At datetime,
	@Description nvarchar(100),
	@UserId int
AS
BEGIN
	--Verify theres no other Folder with same name in user Records
	If NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @UserId)	
		
		RAISERROR('User Not Found', 16, 1);	
	
	ELSE
		If EXISTS(SELECT 1 FROM dbo.[Folder] WHERE dbo.[Folder].Name= @Name AND dbo.[Folder].UserId = @UserId)	
		
		RAISERROR('Folder Already Registered on User Account', 16, 1);	
		
		ELSE
			INSERT INTO dbo.[Folder](dbo.[Folder].Name, dbo.[Folder].Created_At, dbo.[Folder].Description, dbo.[Folder].UserId)
			VALUES(@Name, @Created_At, @Description, @UserId);
END