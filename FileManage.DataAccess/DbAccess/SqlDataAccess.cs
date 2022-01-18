using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManage.DataAccess.DbAccess
{
    public class SqlDataAccess : ISqlDataAccess
    {
        private readonly IConfiguration _config;        

        public SqlDataAccess(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Query on tables in the database Method(GET on Endpoints) using stored procedures
        /// </summary>
        /// <typeparam name="T">Entitie Type</typeparam>
        /// <typeparam name="U">Parameters to send</typeparam>
        /// <param name="storedProcedure">Name of the stored procedure</param>
        /// <param name="parameters">Parameters to send</param>
        /// <param name="connectionId">Connection Id to the databasse</param>
        /// <returns></returns>
        public async Task<IEnumerable<T>> LoadDataAsync<T, U>(
            string storedProcedure, U parameters, string connectionId = "dev")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));

            return await connection.QueryAsync<T>(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }

        /// <summary>
        /// Save entities, update or Delete for Methods(POST, PUTE, DELETE, PATCH on Endpoints)
        /// </summary>
        /// <typeparam name="T">Parameters</typeparam>
        /// <param name="storedProcedure">Name of the stored procedure</param>
        /// <param name="parameters">Parameters to send</param>
        /// <param name="connectionId">Connection Id to the databasse</param>
        /// <returns>VOID</returns>
        public async Task SaveChangesAsync<T>(
            string storedProcedure, T parameters, string connectionId = "dev")
        {
            using IDbConnection connection = new SqlConnection(_config.GetConnectionString(connectionId));
            await connection.ExecuteAsync(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
        }
    }
}
