using TallerWeb.Src.Models;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUsers();
    }
}                   