using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWeb.Src.Models;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetRoleById(int id);

        Task<Role?> GetRoleByName(string name);
    }
}