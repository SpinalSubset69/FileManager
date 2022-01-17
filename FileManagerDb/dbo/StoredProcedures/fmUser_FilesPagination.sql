CREATE PROCEDURE [dbo].[fmUser_FilesPagination]
	@PageSize int,
	@PageIndex int,
	@UserId int
AS
BEGIN		
		SELECT * FROM dbo.[File] 
		WHERE dbo.[File].UserId = @UserId AND dbo.[File].FolderId IS NULL
		ORDER BY dbo.[File].Id   DESC
		OFFSET @PageIndex ROWS FETCH NEXT @PageSize ROWS ONLY		
END
