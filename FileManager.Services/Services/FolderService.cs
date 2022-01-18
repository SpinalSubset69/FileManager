using FileManager.Entities.Dtos.FolderDtos;
using FileManager.Util.Files;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager.Services.Services;

public class FolderService
{
    private readonly IUnitOfWork _db;
    private readonly IConfiguration _configuration;
    private string connectionId = "";

    public FolderService(IUnitOfWork db, IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
        connectionId = _configuration["connectionId"];
    }

    public async Task CreateUserFolder(RegisterFolderDto folder, int userId)
    {
        await _db.Folders.SaveEntityAsync<dynamic>(StoredProcedures.CreateFolder, 
            new { Name = folder.Name, Description = folder.Description, UserId = userId, Created_At = DateTime.Now}, connectionId);
    }

    public async Task<IEnumerable<Folder>> FindFoldersBasedOnUserId(int userId)
    {
        return await _db.Folders.ListAllAsync<dynamic>(StoredProcedures.GetUserFolders, new { UserId = userId }, connectionId);
    }

    public async Task UpdateFolderName(int id, string folderName)
    {
        await _db.Folders.UpdateEntityAsync<dynamic>(StoredProcedures.UpdateFolderName, new { Name = folderName, Id = id}, connectionId);
    }

    public async Task UpdateFolderDesc(int id, string folderName)
    {
        await _db.Folders.UpdateEntityAsync<dynamic>(StoredProcedures.UpdateFolderDesc, new { Description = folderName, Id = id }, connectionId);
    }

    public async Task DeleteFolder(int id, string path)
    {
        var files = await _db.Folders.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFolderFiles, new { FolderId = id}, connectionId);

        if(files != null)
        {
            foreach (var file in files)
            {
                FilesHandler.DeleteFileOnServer(path, file.FileName, file.FileExtension);
            }
        }        

        await _db.Folders.DeleteEntityAsync(StoredProcedures.DeleteFolder, id, connectionId);
    }

    public async Task<IEnumerable<UserFile>> GetFolderFiles(int folderId) =>
        await _db.Folders.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFolderFiles, new { FolderId = folderId }, connectionId);
    
}
