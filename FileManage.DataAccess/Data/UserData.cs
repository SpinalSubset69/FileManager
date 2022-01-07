using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces.Repositories;
using FileManager.Entities.Entities;

namespace FileManage.DataAccess.Data;

public class UserData : GenericRepository<User>, IUserRepository
{
    private readonly ISqlDataAccess _db;
    public UserData(ISqlDataAccess db) : base(db)
    {
        _db = db;
    }

    public async Task<User?> FindUserByEmailAsync(string storedProcedure, string email, string connectionId = "dev")
    {
        var users =  await _db.LoadDataAsync<User, dynamic>(storedProcedure, new { Email = email }, connectionId);
        return users.FirstOrDefault();
    }
  
    public async Task UploadFileAsync(string storedProcedure, UserFile file, string connectionId = "dev") =>
        await _db.SaveChangesAsync<dynamic>(storedProcedure, new
        {
            FileName = file.FileName,
            FileExtension = file.FileExtension,
            FileSize = file.FileSize,
            Created_At = file.Created_At,
            UserId = file.UserId
        }, connectionId);
}
