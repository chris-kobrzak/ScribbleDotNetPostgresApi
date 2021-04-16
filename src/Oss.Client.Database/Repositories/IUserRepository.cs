using System.Threading.Tasks;

namespace Oss.Client.Database.Repositories
{
    public interface IUserRepository
    {
        public bool Exists();
    }
}