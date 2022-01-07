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

    //Folders StoredProcedures
    public const string CreateFolder = "dbo.fmFolder_Insert";
    public const string GetUserFolders = "dbo.fmUser_GetUserFolders";
    public const string UpdateFolderName = "dbo.fmFolder_UpdateFolderName";
    public const string UpdateFolderDesc = "dbo.fmFolder_UpdateDescription";
    public const string DeleteFolder = "dbo.fmFolder_Delete";
}
