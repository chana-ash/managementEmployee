using Manage.Core.Entities;
using Manage.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data.Repositories
{
    public class ManagerRepository:IManagerRepository
    {
        //private readonly DataContext _context;
        //public IUserRepository Users { get; }
        //public IReportRepository Reports { get; }
        //public IVacationRepository Vacations { get; }


        //public ManagerRepository(DataContext context, IUserRepository userRepository,IReportRepository reportRepository,IVacationRepository vacationRepository)
        //{
        //    _context = context;
        //    Users = userRepository;
        //    Reports= reportRepository;
        //    Vacations= vacationRepository;

        //}
        private readonly DataContext _context;
        public IRepository<User> Users { get; }
        public IRepository<Report> Reports { get; }

        public IRepository<Vacation> Vacations { get; }


        public ManagerRepository(DataContext context, IRepository<User> userRepository, IRepository<Report> reportRepository, IRepository<Vacation> vacationRepository)
        {
            _context = context;
            Users = userRepository;
            Reports = reportRepository;
            Vacations = vacationRepository;
        }

        public void Save()
        {
            _context.SaveChangesAsync();
        }
    }
}
