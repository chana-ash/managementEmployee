using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Entities
{
    public class User
    {
        

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Report> Reports { get; set; }
        public List<Vacation> Vacations { get; set; }




    }
}
