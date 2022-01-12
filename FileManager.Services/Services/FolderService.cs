using FileManager.Util.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Services.Services;

public class FolderService
{
    private readonly IUnitOfWork _db;

    public FolderService(IUnitOfWork db)
    {
        _db = db;
    }

    public async Task CreateUserFolder(Folder folder)
    {
        await _db.Folders.SaveEntityAsync<dynamic>(StoredProcedures.CreateFolder, 
            new { Name = folder.Name, Description = folder.Description, UserId = folder.UserId, Created_At = DateTime.Now});
    }

    public async Task<IEnumerable<Folder>> FindFoldersBasedOnUserId(int userId)
    {
        return await _db.Folders.ListAllAsync<dynamic>(StoredProcedures.GetUserFolders, new { UserId = userId });
    }

    public async Task UpdateFolderName(int id, string folderName)
    {
        await _db.Folders.UpdateEntityAsync<dynamic>(StoredProcedures.UpdateFolderName, new { Name = folderName, Id = id});
    }

    public async Task UpdateFolderDesc(int id, string folderName)
    {
        await _db.Folders.UpdateEntityAsync<dynamic>(StoredProcedures.UpdateFolderDesc, new { Description = folderName, Id = id });
    }

    public async Task DeleteFolder(int id, string path)
    {
        var files = await _db.Folders.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFolderFiles, new { FolderId = id});

        if(files != null)
        {
            foreach (var file in files)
            {
                FilesHandler.DeleteFileOnServer(path, file.FileName, file.FileExtension);
            }
        }        

        await _db.Folders.DeleteEntityAsync(StoredProcedures.DeleteFolder, id);
    }

    public async Task<IEnumerable<UserFile>> GetFolderFiles(int folderId) =>
        await _db.Folders.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFolderFiles, new { FolderId = folderId });
    
}
