CREATE TABLE [dbo].[File]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [FileName] VARCHAR(MAX) NULL, 
    [FileExtension] NVARCHAR(50) NULL, 
    [Created_At] DATETIME NULL, 
    [FolderId] INT NOT NULL, 
    [UserId] INT NULL,
    [FileSize] VARCHAR(MAX) NULL, 
    CONSTRAINT FK_Folder_File FOREIGN KEY (FolderId) REFERENCES dbo.[Folder](Id) ON DELETE CASCADE,
    CONSTRAINT FK_User_File FOREIGN KEY (UserId) REFERENCES dbo.[User](Id)
)
