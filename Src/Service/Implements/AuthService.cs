using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using TallerWeb.Src.DTOs.User;
using TallerWeb.Src.Models;
using TallerWeb.Src.Repositories.Interfaces;
using TallerWeb.Src.Service.Interfaces;

namespace TallerWeb.Src.Service.Implements
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUserRepository _userRepository;
        private readonly IMapperService _mapperService;
        private readonly IRoleRepository _roleRepository;
        private readonly IGenderRepository _genderRepository;

        public AuthService(IConfiguration configuration, IUserRepository userRepository, IMapperService mapperService, IRoleRepository roleRepository, IGenderRepository genderRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _mapperService = mapperService;
            _roleRepository = roleRepository;
            _genderRepository = genderRepository;
        }

        public async Task<string> LoginUser(LoginUserDto loginUserDto)
        {
            string mensaje = "Credenciales invalidas";
            var user = await _userRepository.GetUserByEmail(loginUserDto.Email.ToString());
            if (user is null)
            {
                return mensaje;
            }

            var resultado = BCrypt.Net.BCrypt.Verify(loginUserDto.Password, user.Password);
            if(!resultado){return mensaje;}
            
            
            var token = GenerateToken(user);
            return token;
           
        }

        public async Task<string> RegisterUser(RegisterUserDto registerUserDto)
        {
            registerUserDto.Rut = registerUserDto.Rut.ToUpper();
            var mappedUser =  _mapperService.RegisterUserDtoToUser(registerUserDto);
            if (_userRepository.VerifyUserByEmail(mappedUser.Email).Result)
            {
                return "El email ya esta registrado";
            }

            var salt = BCrypt.Net.BCrypt.GenerateSalt(12);
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(registerUserDto.Password, salt);
            mappedUser.Password = passwordHash;
            

            var role =  await _roleRepository.GetRoleByName("Cliente");

            if(role == null)
            {
                return "El rol no existe";
            }
            mappedUser.RoleId = role.Id;

            var gender = await _genderRepository.GetGenderById(registerUserDto.GenderId);
        
            if(gender == null){
                return "El genero no existe";
            }
            mappedUser.GenderId = gender.Id;
            

            await _userRepository.CreateUser(mappedUser); 
            var user = await  _userRepository.GetUserByEmail(mappedUser.Email);
            if (user != null)
            {
                var token = GenerateToken(user);
                return token;
            }
            return "Error al registrar el usuario";
        
           

        }
    

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>
            {
                new ("Id", user.Id.ToString()),
                new ("Email", user.Email),
                new (ClaimTypes.Role, user.Role.Nombre)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(2),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }

        
    }
}
