using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Entities.Enums;

public static class StoredProcedures
{
    //Users StoredProcedures
    public const string SaveUser = "dbo.fmUser_Insert";
    public const string GetUserById = "dbo.fmUser_GetUser";
    public const string UpdateUserInfo = "dbo.fmUser_UpdateUserInfo";
    public const string DeleteUser = "dbo.fmUser_DeleteUser";
    public const string GetUserByEmail = "dbo.fmUser_GetUserByEmail";
    public const string UploadFile = "dbo.fmFile_Insert";
    public const string InsertFileIntoFolder = "dbo.fmFile_InsertFileIntoFolder";
    public const string GetUserFolders = "dbo.fmUser_GetUserFolders";
    public const string GetUserFIles = "dbo.fmUser_GetUserFilesOutFolder";
    public const string DeleteFile = "dbo.fmFile_Delete";
    public const string GetAllUserFiles = "dbo.fmUser_GetAllUserFiles";
    public const string UploadUserImage = "dbo.fmUser_UpdateInsertUserImage";
    public const string GetFilesWithPagination = "dbo.fmUser_FilesPagination";
    public const string GetFilesCount = "dbo.fmUser_UserFilesCount";
    public const string QueryOnUserFiles= "dbo.fmUser_QueryOnFiles";
    public const string GetFileByName = "dbo.fmFile_GetFileByName";
    public const string UpdateSpaceInUse = "dbo.fmUser_UpdateSpaceInUse";

    //Folders StoredProcedures
    public const string CreateFolder = "dbo.fmFolder_Insert";    
    public const string UpdateFolderName = "dbo.fmFolder_UpdateFolderName";
    public const string UpdateFolderDesc = "dbo.fmFolder_UpdateDescription";
    public const string DeleteFolder = "dbo.fmFolder_Delete";
    public const string GetFolderFiles = "dbo.fmUser_GetUserFolderFiles";

    //Download StoredProcedures
    public const string GetFileInfo = "dbo.fmFile_Get";
}
