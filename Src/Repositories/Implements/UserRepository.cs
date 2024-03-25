using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.Data;
using TallerWeb.Src.Repositories.Interfaces;
using TallerWeb.Src.Models;

namespace TallerWeb.Src.Repositories.Implements
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            return users;
        }
    }
}