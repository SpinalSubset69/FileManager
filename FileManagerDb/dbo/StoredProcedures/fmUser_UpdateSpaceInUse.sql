CREATE PROCEDURE [dbo].[fmUser_UpdateSpaceInUse]
	@Id int ,
	@SpaceInUse varchar(MAX)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM dbo.[User] WHERE dbo.[User].Id = @Id)
			RAISERROR('User Not Found on Database', 16, 1);
		ELSE
			UPDATE dbo.[User] SET dbo.[User].SpaceInUse = @SpaceInUse
			WHERE dbo.[User].Id = @Id
END