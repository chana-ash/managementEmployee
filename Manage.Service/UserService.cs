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
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

       
        public async Task<User?> AuthenticateAsync(string userName, string password)
        {
            var users = await _userRepository.GetAllAsync(); 
            var user = users.FirstOrDefault(u => u.Name == userName);
            if (user != null && password == user.Password)
            {
                return user;
            }
            return null;
        }

        
        public async Task<List<User>> GetAllAsync()
        {
            var list = await _userRepository.GetAllAsync(); 
            return list.ToList();
        }

        
        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id); 
        }

       
        public async Task<User> AddAsync(User user)
        {
            return await _userRepository.AddAsync(user); 
        }

        
        public async Task<User> UpdateAsync(User user)
        {
            return await _userRepository.UpdateAsync(user); 
        }

        
        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user != null)
            {
                await _userRepository.DeleteAsync(user);
            }


        }
        //public async Task<User?> Authenticate(string email, string password)
        //{
        //    // כאן את כותבת את הלוגיקה למציאת יוזר לפי אימייל וסיסמה
        //}

    }

}
