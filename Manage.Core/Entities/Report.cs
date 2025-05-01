using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Entities
{
    public class Report
    {
        public int Id { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime WorkDate { get; set; }    
        public DateTime StartTime { get; set; }   
        public DateTime EndTime { get; set; }
    }
}
