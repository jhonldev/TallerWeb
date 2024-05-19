using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TallerWeb.Src.Data;
using TallerWeb.Src.Models;
using TallerWeb.Src.Repositories.Interfaces;

namespace TallerWeb.Src.Repositories.Implements
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DataContext _context;

        public RoleRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Role?> GetRoleById(int id)
        {
            var role = await _context.Roles.FindAsync(id);
            return role;
        }

        public async Task<Role?> GetRoleByName(string nombre)
        {
            var role = await _context.Roles.Where(r => r.Nombre == nombre).FirstOrDefaultAsync();
            return role;
        }
    }
}