CREATE PROCEDURE [dbo].[fmUser_UserFilesCount]
	@UserId int
AS
BEGIN
	SELECT COUNT(*) AS TOTAL FROM dbo.[File]
	WHERE dbo.[File].UserId = @UserId AND dbo.[File].FolderId IS NULL
END
