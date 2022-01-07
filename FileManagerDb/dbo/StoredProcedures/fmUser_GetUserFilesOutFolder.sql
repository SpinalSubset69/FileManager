CREATE PROCEDURE [dbo].[fmUser_GetUserFilesOutFolder]
	@Id int
AS
BEGIN
	SELECT * FROM dbo.[File] 
	WHERE dbo.[File].UserId = @Id AND dbo.[File].FolderId = null
END
