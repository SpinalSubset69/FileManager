﻿using FileManager.Entities.Dtos;
using FileManager.Util.Files;
using Microsoft.Extensions.Configuration;

namespace FileManager.Services.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _db;                

        public UserService(IUnitOfWork db)
        {
            _db = db;            
        }

        public async Task SaveUserAsync(User user)
        {
            //Encrypt User Password
            Util.Encrypt.EncryptHMAC.GetHMAC512(user.Password, out string hassedPassword, out string hashedSalt);
            user.Password = hassedPassword;
            user.PasswordSalt = hashedSalt;

            //Save on the db
            await _db.Users.SaveEntityAsync<dynamic>(StoredProcedures.SaveUser, 
                new { UserName = user.UserName, Email = user.Email,  Password = user.Password, PasswordSalt = user.PasswordSalt, Created_At = DateTime.Now});
        }

        public async Task<User?> FindUserById(int id)
        {
            return await _db.Users.FindByIdAsync(StoredProcedures.GetUserById, id);            
        }

        public async Task UpdateUserInfo(int id, UpdateUserInfoDto request)
        {
            var user = await FindUserById(id);

            await _db.Users.UpdateEntityAsync<dynamic>(StoredProcedures.UpdateUserInfo, new { UserName = request.UserName, Email = request.Email, Id = user.Id });
        }

        public async Task DeleteUser(int id)
        {
            await _db.Users.DeleteEntityAsync(StoredProcedures.DeleteUser, id);
        }

        public async Task UploadFile(int id, FileUploadRequest file, string webContentPath)
        {
            var user = await FindUserById(id);
            var fileInfo = await FilesHandler.WriteFileOnServer(webContentPath, file);
            fileInfo.UserId = user.Id;
            await _db.Users.UploadFileAsync(StoredProcedures.UploadFile, fileInfo);
        }

        public async Task InsertFileIntoFolder(int fileId, int folderId, int userId)
        {
            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.InsertFileIntoFolder, new
            {
                Id = fileId,
                UserId = userId,
                FolderId = folderId
            });
        }


        public async Task<IEnumerable<dynamic>?> GetUserFilesOrFolders(int userId, string target)
        {
            return target.ToLower() switch
            {
                "folders" => await _db.Users.ExecuteEntityQueriesAsync<Folder, dynamic>(StoredProcedures.GetUserFolders, new { UserId = userId }),
                "files" => await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetUserFIles, new { Id = userId }),
                _ => null
            };
        }
    }
}