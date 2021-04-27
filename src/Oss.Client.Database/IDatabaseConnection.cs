using System.Threading.Tasks;
using Npgsql;

namespace Oss.Client.Database
{
    public interface IDatabaseConnection
    {
        public Task<NpgsqlConnection> Get();
    }
}