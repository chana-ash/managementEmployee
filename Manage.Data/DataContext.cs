using Manage.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Data
{
    public class DataContext:DbContext
    {
        public DbSet<User> users {  get; set; }
        public DbSet<Report> report { get; set; }
        public DbSet<Vacation> vacations { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=sample_db");
        }
    }
}
