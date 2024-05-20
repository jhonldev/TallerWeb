using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWeb.Src.Data;
using TallerWeb.Src.Models;
using TallerWeb.Src.Repositories.Interfaces;

namespace TallerWeb.Src.Repositories.Implements
{
    public class GenderRepository : IGenderRepository
    {
        private readonly DataContext _context;
        
        public GenderRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Gender?> GetGenderById(int id)
        {
            var gender = await _context.Gender.FindAsync(id);
            return gender;
        }
    }
}