using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWeb.Src.DTOs.User;

namespace TallerWeb.Src.Service.Interfaces
{
    public interface IAuthService
    {
        Task<string> RegisterUser(RegisterUserDto registerUserDto); 
        Task<string> LoginUser(LoginUserDto loginUserDto);
    }
}