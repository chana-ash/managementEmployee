using Manage.Core.Entities;
using Manage.Core.Repositories;
using Manage.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Service
{
    public class VacationService : IVacationService
    {
        private readonly IRepository<Vacation> _vacationRepository;

        public VacationService(IRepository<Vacation> vacationRepository)
        {
            _vacationRepository = vacationRepository;
        }

        public async Task<List<Vacation>> GetAllAsync() 
        {
            var list= await _vacationRepository.GetAllAsync();
            return list.ToList();
           
        }

        public async Task<Vacation?> GetByIdAsync(int id) 
        {
            return await _vacationRepository.GetByIdAsync(id); 
        }

        public async Task<Vacation> AddAsync(Vacation vacation) 
        {
            return await _vacationRepository.AddAsync(vacation);
         }

        public async Task<Vacation> UpdateAsync(Vacation vacation) 
        {
            return await _vacationRepository.UpdateAsync(vacation); 
        }

        public async Task DeleteAsync(int id) 
        {
            
            var vacation = await _vacationRepository.GetByIdAsync(id);
            if (vacation != null)
            {
                await _vacationRepository.DeleteAsync(vacation);
            }
        }
    }

}
