using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Entities
{
    public class Vacation
    {
        public int Id { get; set; }
        public User User { get; set; }

        public int UserId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Request { get; set; }
    }
}
