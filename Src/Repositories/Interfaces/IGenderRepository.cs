using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TallerWeb.Src.Models;

namespace TallerWeb.Src.Repositories.Interfaces
{
    public interface IGenderRepository
    {
        Task<Gender?> GetGenderById(int id);
    }
}