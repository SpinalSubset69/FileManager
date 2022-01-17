CREATE PROCEDURE [dbo].[fmUser_QueryOnFiles]
	@Query varchar(500),
	@Id int
AS
BEGIN
	SELECT * FROM dbo.[File] WHERE dbo.[File].FileName LIKE '%' + @Query + '%' AND dbo.[File].UserId = @Id
END
