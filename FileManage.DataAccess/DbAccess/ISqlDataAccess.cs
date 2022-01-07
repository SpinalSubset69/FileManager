
namespace FileManage.DataAccess.DbAccess;

public interface ISqlDataAccess
{
    Task<IEnumerable<T>> LoadDataAsync<T, U>(string storedProcedure, U parameters, string connectionId = "dev");
    Task SaveChangesAsync<T>(string storedProcedure, T parameters, string connectionId = "dev");
}