using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManage.DataAccess.Interfaces;

public interface IRepository<T>
{
    Task SaveEntityAsync<U>(string storedProcedure, U entity, string connectionId = "dev");
    Task DeleteEntityAsync(string storedProcedure, int id, string connectionId = "dev");
    Task UpdateEntityAsync<U>(string storedProcedure, U entity, string connectionId = "dev");
    Task<IEnumerable<T>> ListAllAsync<U>(string storedProcedure, U parameters ,string connectionId = "dev");
    Task<T?> FindByIdAsync(string storedProcedure, int id, string connectionId = "dev");
    Task ExecuteEntityCommandsAsync<U>(string storedProcedure, U parameters, string connectionId = "dev");
    Task<IEnumerable<T>> ExecuteEntityQueriesAsync<T,U>(string storedProcedure, U parameters, string connectionId = "dev");
}
