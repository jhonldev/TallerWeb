using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TallerWeb.Src.DTOs.User;
using TallerWeb.Src.Repositories.Interfaces;
using TallerWeb.Src.Service.Interfaces;

namespace TallerWeb.Src.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        

        public async Task<bool> ChangePassword(int id, ChangePasswordDto changePasswordDto)
        {
            var user = await _userRepository.GetUserById(id);
            if (user == null || changePasswordDto.NewPassword != changePasswordDto.ConfirmPassword)
            {
                return false;
            }


            if(!BCrypt.Net.BCrypt.Verify(changePasswordDto.ActualPassword, user.Password))
            {
                return false;
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(changePasswordDto.NewPassword);
            changePasswordDto.NewPassword = hashedPassword;

            await _userRepository.ChangePassword(id, changePasswordDto);
            return true;
            
        }

        public Task<bool> DeleteUser(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> EditUser(int id, EditUserDto editUserDto)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return false;
            }
            existingUser.Nombre = editUserDto.Nombre ?? existingUser.Nombre;
            if(editUserDto.FechaNacimiento != DateTime.MinValue)
            {
                existingUser.FechaNacimiento = editUserDto.FechaNacimiento;
            }
            if(editUserDto.GenderId != 0)
            {
                existingUser.GenderId = editUserDto.GenderId;
            }
            await _userRepository.EditUser(id, editUserDto);
            return true;
        }

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var users = await _userRepository.GetUsers();
            var mappedUsers = new List<UserDto>();
            for (int i = 0; i < users.Count() ; i++)
            {
                var userDto = _mapper.Map<UserDto>(users.ElementAt(i));
                mappedUsers.Add(userDto);
            }
            return mappedUsers;

        }
}   }  