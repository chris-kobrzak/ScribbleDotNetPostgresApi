using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Npgsql;

namespace Oss.Client.Database
{
    public class PostgresConnection : IDatabaseConnection
    {
        private readonly IPostgresClientConfig _clientConfig;
        private string _connString { get; set; }

        public PostgresConnection(IOptions<PostgresClientConfig> clientConfig)
        {
            _clientConfig = clientConfig.Value;

            _connString = BuildConnectionString();
        }

        public async Task<NpgsqlConnection> Get()
        {
            var connection = new NpgsqlConnection(_connString);
            await connection.OpenAsync();

            return connection;
        }

        private string BuildConnectionString()
        {
            var builder = new Npgsql.NpgsqlConnectionStringBuilder();

            builder.Host = _clientConfig.Host;
            builder.Port = _clientConfig.Port;
            builder.Database = _clientConfig.Name;
            builder.Username = _clientConfig.User;
            builder.Password = _clientConfig.Password;

            return builder.ConnectionString;
        }
    }
}