using Manage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Services
{
    public interface IReportService
    {
        Task<List<Report>> GetAllAsync();
        Task<Report?> GetByIdAsync(int id);
        Task<Report> AddAsync(Report report);
        Task<Report> UpdateAsync(Report report);
        Task DeleteAsync(int id);
    }
}
