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

        public async Task<bool> DeleteUser(int id)
        {
            var result = await _userRepository.DeleteUser(id);
            return result;
        }

        public async Task<bool> EditUser(int id, EditUserDto editUserDto)
        {
            var existingUser = await _userRepository.GetUserById(id);
            if (existingUser == null)
            {
                return false;
            }
    
            await _userRepository.EditUser(id, editUserDto);
            return true;
        }

        public async Task<IEnumerable<UserDto>> FindUsers(string query)
        {
            var users = await _userRepository.FindUsers(query);
            var userDtos = users.Select(u => _mapper.Map<UserDto>(u));
            return userDtos;
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
    }

        
}     