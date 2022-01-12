using FileManager.Entities.Dtos;
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

        public async Task SaveUserImageAsync(int id, FileUploadRequest file, string path)
        {
            var fileSaved = await FilesHandler.WriteFileOnServer(path, file);
            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.UploadUserImage, new
            {
                Id = id,
                ProfileImage = fileSaved.FileName + "." + fileSaved.FileExtension
            });
        }

        public async Task SaveUserAsync(RegisterUserDto user)
        {
            //Encrypt User Password
            Util.Encrypt.EncryptHMAC.GetHMAC512(user.Password, out byte[] hashedPassword, out byte[] hashedSalt);
            
            //Save on the db
            await _db.Users.SaveEntityAsync<dynamic>(StoredProcedures.SaveUser, 
                new { UserName = user.UserName, Email = user.Email,  Password = hashedPassword, PasswordSalt = hashedSalt, Created_At = DateTime.Now});
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

        public async Task DeleteUser(int id, string path)
        {
            var userFiles = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetAllUserFiles, new { Id = id });

            foreach (var file in userFiles)
            {
                FilesHandler.DeleteFileOnServer(path, file.FileName, file.FileExtension);
            }

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

        public async Task<User?> VerifyLoginInfo(LoginDto info)
        {
            var users = await _db.Users.ExecuteEntityQueriesAsync<User, dynamic>(StoredProcedures.GetUserByEmail, new { email = info.Email});
            var user = users.FirstOrDefault();

            if(!Util.Encrypt.EncryptHMAC.CompareHMAC512(info.Password, user.Password, user.PasswordSalt))
            {
                throw new ApplicationException("Invalid Credentials");
            }

            return user;
        }

        public async Task<FileInfoResponse> GetFileStreamBasedOnId(int id, string contentRootPath)
        {
            var files = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFileInfo,new
            {
                Id = id
            });
            var fileInfo = files.FirstOrDefault();
            return await FilesHandler.GetFileBytes(contentRootPath, fileInfo);
        }

        public async Task DeleteUserFileAsync(int fileId, string contentRootPath)
        {            
            var files = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFileInfo, new { Id = fileId });
            var file = files.FirstOrDefault();            

            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.DeleteFile, new { Id = fileId });
            FilesHandler.DeleteFileOnServer(contentRootPath, file.FileName, file.FileExtension);
        }
    }
}
