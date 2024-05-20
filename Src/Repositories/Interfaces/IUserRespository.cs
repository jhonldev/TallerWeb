using TallerWeb.Src.DTOs.User;
using TallerWeb.Src.Models;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task CreateUser(User user);
        Task<IEnumerable<User>> GetUsers();
        Task<User?> GetUserByEmail(string email);
        Task<bool> EditUser(int id, EditUserDto editUserDto);
        Task<bool> VerifyUserByEmail(string email);
        Task<User?> GetUserById(int id);
        Task ChangePassword(int id, ChangePasswordDto changePasswordDto);
        Task <bool> DeleteUser(int id);
        Task<IEnumerable<User>> FindUsers(string query);
    }
}                   