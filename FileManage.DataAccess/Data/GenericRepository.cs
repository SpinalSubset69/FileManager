using FileManage.DataAccess.DbAccess;
using FileManage.DataAccess.Interfaces;

namespace FileManage.DataAccess.Data;

public class GenericRepository<T> : IRepository<T>
{
    private readonly ISqlDataAccess _db;
    public GenericRepository(ISqlDataAccess db)
    {
        _db = db;
    }

    public Task DeleteEntityAsync(string storedProcedure, int id, string connectionId = "dev") =>
        _db.SaveChangesAsync<dynamic>(storedProcedure, new { Id = id }, connectionId);

    public async Task ExecuteEntityCommandsAsync<U>(string storedProcedure, U parameters, string connectionId = "dev")
    {
        await _db.SaveChangesAsync(storedProcedure, parameters, connectionId);
    }

    public async Task<IEnumerable<T>> ExecuteEntityQueriesAsync<T, U>(string storedProcedure, U parameters, string connectionId = "dev")
    {
        return await _db.LoadDataAsync<T, U>(storedProcedure, parameters, connectionId);
    }

    public async Task<T?> FindByIdAsync(string storedProcedure, int id, string connectionId = "dev")
    {
        var entities =  await _db.LoadDataAsync<T, dynamic>(storedProcedure, new { Id = id }, connectionId);
        return entities.FirstOrDefault();
    }

    public async Task<IEnumerable<T>> ListAllAsync<U>(string storedProcedure, U parameters, string connectionId = "dev") =>
        await _db.LoadDataAsync<T, U>(storedProcedure, parameters, connectionId);

    public Task SaveEntityAsync<U>(string storedProcedure, U entity, string connectionId = "dev") =>
        _db.SaveChangesAsync(storedProcedure,  entity , connectionId);

    public Task UpdateEntityAsync<U>(string storedProcedure, U entity, string connectionId = "dev") =>
        _db.SaveChangesAsync(storedProcedure, entity, connectionId);
}
