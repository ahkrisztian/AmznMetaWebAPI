using BenchmarkDotNet.Configs;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Microsoft.Data.Sqlite;

namespace AmznMetaLibrary.Repo.Data
{
    public class SqliteDataAccess : ISqliteDataAccess
    {
        private readonly IConfiguration config;

        public SqliteDataAccess(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string sqlstatement, U parameters, string connectionStringName)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqliteConnection(connectionString);

            var rows = await connection.QueryAsync<T>(sqlstatement, parameters);

            return rows.ToList();
        }

        public async Task SaveData<T>(string sqlstatement, T parameters, string connectionStringName)
        {
            string connectionString = config.GetConnectionString(connectionStringName);

            using IDbConnection connection = new SqlConnection(connectionString);

            await connection.ExecuteAsync(sqlstatement, parameters);

        }
    }
}
