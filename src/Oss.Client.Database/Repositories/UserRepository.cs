using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Npgsql;
using Oss.Core.Models;

namespace Oss.Client.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private const string activeUsersQuery = @"select id, name from get_users(:pActive)";

        private readonly IDatabaseConnection _dbConn;

        public UserRepository(IDatabaseConnection dbConn)
        {
            _dbConn = dbConn;
        }

        public async Task<IEnumerable<Dictionary<string, object>>> GetAllActive()
        {
            using var connection = await _dbConn.Get();

            NpgsqlCommand executor = new NpgsqlCommand(activeUsersQuery, connection);
            executor.CommandType = CommandType.Text;
            executor.Parameters.AddWithValue("pActive", true);

            using var reader = await executor.ExecuteReaderAsync();

            return Serialise(reader);
        }

        private IEnumerable<Dictionary<string, object>> Serialise(NpgsqlDataReader reader)
        {
            var results = new List<Dictionary<string, object>>();
            var columns = new List<string>();

            for (var i = 0; i < reader.FieldCount; i++)
                columns.Add(reader.GetName(i));

            while (reader.Read())
                results.Add(SerialiseRow(columns, reader));

            return results;
        }

        private Dictionary<string, object> SerialiseRow(IEnumerable<string> columns,
            NpgsqlDataReader reader)
        {
            var result = new Dictionary<string, object>();

            foreach (var column in columns)
                result.Add(column, reader[column]);

            return result;
        }
    }
}