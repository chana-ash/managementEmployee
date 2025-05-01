using Manage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Services
{
    public interface IVacationService
    {



        Task<List<Vacation>> GetAllAsync();
            Task<Vacation?> GetByIdAsync(int id);
            Task<Vacation> AddAsync(Vacation vacation);
            Task<Vacation> UpdateAsync(Vacation vacation);
            Task DeleteAsync(int id);
        
    }

}

