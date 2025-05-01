using Manage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Repositories
{
    public interface IManagerRepository
    {
        //IUserRepository Users { get; }
        //IReportRepository Reports { get; }
        //IVacationRepository Vacations { get; }
         IRepository<User> Users { get; }
         IRepository<Report> Reports { get; }

         IRepository<Vacation> Vacations { get; }
        void Save();
    }
}
