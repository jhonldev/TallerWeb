using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.Data;
using TallerWeb.Src.Repositories.Interfaces;
using TallerWeb.Src.Models;
using TallerWeb.Src.DTOs.User;

namespace TallerWeb.Src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task ChangePassword(int id, ChangePasswordDto changePasswordDto)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return;
            }
            existingUser.Password = changePasswordDto.NewPassword;
            _context.Entry(existingUser).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        
        }

        async public Task CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteUser(int id)
        {
            var existingUser = await _context.Users.FindAsync(id);
            if (existingUser == null)
            {
                return false;
            }
            _context.Users.Remove(existingUser);
            await _context.SaveChangesAsync();
            return true;
        }

        async public Task<bool> EditUser(int id, EditUserDto editUserDto)
        {
            var existingUser = await _context.Users.FindAsync(id);
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
            
            await _context.SaveChangesAsync();
            return true;
        }

        async public Task<User?> GetUserByEmail(string email)
        {
            var user =await _context.Users.Where(u => u.Email == email)
                                            .Include(u => u.Role)
                                            .FirstOrDefaultAsync();

            return user;
        }

        public async Task<User?> GetUserById(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user;
        }


        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }

        public async Task<bool> VerifyUserByEmail(string email)
        {
            var user = await _context.Users.Where(u => u.Email == email).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}