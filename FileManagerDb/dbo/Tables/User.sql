﻿CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [UserName] NVARCHAR(50) NOT NULL, 
    [Email] NVARCHAR(50) NOT NULL, 
    [Password] VARCHAR(MAX) NOT NULL, 
    [PasswordSalt] VARCHAR(MAX) NOT NULL, 
    [Created_At] DATETIME NULL, 
    [ProfileImage] VARCHAR(255) NULL
)
