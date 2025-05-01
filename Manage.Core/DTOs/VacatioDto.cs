using Manage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.DTOs
{
    public class VacationDto
    {
        public int Id { get; set; }
        

        public int UserId { get; set; }
        public DateTime ReportDate { get; set; }
        public string Request { get; set; }
    }
}
