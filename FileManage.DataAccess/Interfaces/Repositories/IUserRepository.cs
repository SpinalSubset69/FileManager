using FileManager.Entities.Entities;

namespace FileManage.DataAccess.Interfaces.Repositories;

public interface IUserRepository : IRepository<User>
{
    Task<User?> FindUserByEmailAsync(string storedProcedure, string email, string connectionId = "dev");
    Task UploadFileAsync(string storedProcedure, UserFile file, string connectionId = "dev");
}
