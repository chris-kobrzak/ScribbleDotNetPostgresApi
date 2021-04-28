using System.Collections.Generic;
using System.Threading.Tasks;
using Oss.Core.Models;

namespace Oss.Client.Database.Repositories
{
    public interface IUserRepository
    {
        public Task<User> GetByCredentials(string Email, string Password);
        public Task<IEnumerable<Dictionary<string, object>>> GetAllActive();
    }
}