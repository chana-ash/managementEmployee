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
    public class ReportService : IReportService
    {
        private readonly IRepository<Report> _reportRepository;

        public ReportService(IRepository<Report> reportRepository)
        {
            _reportRepository = reportRepository;
        }

        public async Task<List<Report>> GetAllAsync()
        {
            var reports = await _reportRepository.GetAllAsync();
            return reports.ToList(); 
        }

        public async Task<Report?> GetByIdAsync(int id)
        {
            return await _reportRepository.GetByIdAsync(id);
        }

        public async Task<Report> AddAsync(Report report)
        {
            return await _reportRepository.AddAsync(report);
        }

        public async Task<Report> UpdateAsync(Report report)
        {
            return await _reportRepository.UpdateAsync(report);
        }

        public async Task DeleteAsync(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report != null)
            {
                await _reportRepository.DeleteAsync(report);
            }
        }
    }

}
