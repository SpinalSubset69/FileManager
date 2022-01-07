CREATE PROCEDURE [dbo].[fmUser_GetUserFolderFiles]
	@UserId int,
	@FolderId int
AS
BEGIN
	SELECT * FROM dbo.[File] WHERE dbo.[File].UserId = @UserId AND dbo.[File].FolderId = @FolderId
END
