using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace Oss.Client.Database.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PostgresClientConfig _dbConfig;

        public UserRepository(IOptions<PostgresClientConfig> dbConfig)
        {
            _dbConfig = dbConfig.Value;
        }

        public bool Exists()
        {
            return true;
        }
    }
}