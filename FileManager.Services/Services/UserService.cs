using FileManager.Entities.Dtos;
using FileManager.Util.Files;
using Microsoft.Extensions.Configuration;

namespace FileManager.Services.Services
{
    public class UserService
    {
        private readonly IUnitOfWork _db;     
        private readonly IConfiguration _configuration;
        private string connectionId = "";
        public UserService(IUnitOfWork db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
            connectionId = _configuration["connectionId"];
        }

        public async Task SaveUserImageAsync(int id, FileUploadRequest file, string path)
        {
            var user = await FindUserById(id);
            var fileSaved = await FilesHandler.SaveUserImageOnServer(user, path, file);
            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.UploadUserImage, new
            {
                Id = id,
                ProfileImage = fileSaved.FileName + "." + fileSaved.FileExtension
            }, connectionId);
        }        

        public async Task<IEnumerable<UserFile>> QueryOnFilesAsync(int id, string query)
        {
            var files = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.QueryOnUserFiles, new
            {
                Id = id,
                Query = query
            }, connectionId);

            return files;
        }

        public async Task<User> SaveUserAsync(RegisterUserDto user)
        {
            //Encrypt User Password
            Util.Encrypt.EncryptHMAC.GetHMAC512(user.Password, out byte[] hashedPassword, out byte[] hashedSalt);            
            //Save on the db
            await _db.Users.SaveEntityAsync<dynamic>(StoredProcedures.SaveUser, 
                new { UserName = user.UserName, Email = user.Email.ToLower(),  Password = hashedPassword, PasswordSalt = hashedSalt, Created_At = DateTime.Now},
                connectionId);

            var users = await _db.Users.ExecuteEntityQueriesAsync<User, dynamic>(StoredProcedures.GetUserByEmail, new { email = user.Email }, connectionId);

            return users.FirstOrDefault();
        }

        public async Task<User?> FindUserById(int id)
        {
            return await _db.Users.FindByIdAsync(StoredProcedures.GetUserById, id, connectionId);            
        }

        public async Task UpdateUserInfo(int id, UpdateUserInfoDto request)
        {
            var user = await FindUserById(id);

            await _db.Users.UpdateEntityAsync<dynamic>(StoredProcedures.UpdateUserInfo, new { UserName = request.UserName, Email = request.Email, Id = user.Id }, connectionId);
        }

        public async Task DeleteUser(int id, string path)
        {
            var userFiles = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetAllUserFiles, new { Id = id }, connectionId);

            foreach (var file in userFiles)
            {
                FilesHandler.DeleteFileOnServer(path, file.FileName, file.FileExtension);
            }

            await _db.Users.DeleteEntityAsync(StoredProcedures.DeleteUser, id, connectionId);
        }

        public async Task<UserFile> UploadFile(int id, FileUploadRequest file, string webContentPath)
        {
            var user = await FindUserById(id);

            var newSpaceInUse = FilesHandler.CalculateNewSpaceInUse(Convert.ToDouble(user.SpaceInUse), file.Content);

            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.UpdateSpaceInUse, new { Id = id, SpaceInUse = newSpaceInUse }, connectionId);

            var fileInfo = await FilesHandler.WriteFileOnServer(webContentPath, file);            
            fileInfo.UserId = user.Id;

            await _db.Users.UploadFileAsync(StoredProcedures.UploadFile, fileInfo, connectionId);
            
            var userFiles = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFileByName, new
            {
                FileName = fileInfo.FileName
            }, connectionId);

            return userFiles.FirstOrDefault();
        }

        public async Task InsertFileIntoFolder(int fileId, int folderId, int userId)
        {
            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.InsertFileIntoFolder, new
            {
                Id = fileId,
                UserId = userId,
                FolderId = folderId
            }, connectionId);
        }

        public async Task<IEnumerable<dynamic>?> GetUserFilesOrFolders(int userId, string target)
        {
            return target.ToLower() switch
            {
                "folders" => await _db.Users.ExecuteEntityQueriesAsync<Folder, dynamic>(StoredProcedures.GetUserFolders, new { UserId = userId }, connectionId),

                "filesoutfolder" => await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetUserFIlesOutFolder,                 
                new { Id = userId}, connectionId),

                "files" => await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetUserFiles,                 
                new { Id = userId}, connectionId),

                _ => null
            };
        }

        public async Task<User?> VerifyLoginInfo(LoginDto info)
        {
            var users = await _db.Users.ExecuteEntityQueriesAsync<User, dynamic>(StoredProcedures.GetUserByEmail, new { email = info.Email}, connectionId);
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
            }, connectionId);
            var fileInfo = files.FirstOrDefault();
            return await FilesHandler.GetFileBytes(contentRootPath, fileInfo);
        }

        public async Task DeleteUserFileAsync(int id,int fileId, string contentRootPath)
        {   
            var user = await FindUserById(id);
            var files = await _db.Users.ExecuteEntityQueriesAsync<UserFile, dynamic>(StoredProcedures.GetFileInfo, new { Id = fileId }, connectionId);
            var file = files.FirstOrDefault();

            var newSpaceInUse = FilesHandler.CalculateNewSpaceInUse(Convert.ToDouble(user.SpaceInUse), null, file.FileSize);            

            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.UpdateSpaceInUse, new { Id = user.Id, SpaceInUse = newSpaceInUse }, connectionId);
            await _db.Users.ExecuteEntityCommandsAsync<dynamic>(StoredProcedures.DeleteFile, new { Id = fileId }, connectionId);

            FilesHandler.DeleteFileOnServer(contentRootPath, file.FileName, file.FileExtension);
        }
    }
}
