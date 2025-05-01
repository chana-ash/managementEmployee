using Manage.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manage.Core.Services
{
    public interface IUserService
    {
        Task<User?> AuthenticateAsync(string userName, string password);
        Task<List<User>> GetAllAsync();
        Task<User?> GetByIdAsync(int id);
        Task<User> AddAsync(User user);
        Task<User> UpdateAsync(User user);
        Task DeleteAsync(int id);
        //Task<User?> Authenticate(string email, string password);


    }
}
