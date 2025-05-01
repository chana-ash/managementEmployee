using AutoMapper;
using Manage.Core.DTOs;
using Manage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Report, ReportDto>().ReverseMap();
            CreateMap<Vacation, VacationDto>().ReverseMap();

        }
    }
}
