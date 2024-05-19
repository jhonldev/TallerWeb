using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWeb.Src.DTOs.User;

namespace TallerWeb.Src.Service.Interfaces
{
    public interface IUserService
    {
        public Task<IEnumerable<UserDto>> GetUsers();
        public Task<bool> EditUser(int id, EditUserDto editUserDto);
        public Task<bool> DeleteUser(int id);
        public Task<bool> ChangePassword(int id, ChangePasswordDto changePasswordDto);
    }
}