using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;
using TaskManager.Data.Repositories;

namespace TaskManager.Services
{
    public class UserService
    {
        private UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> Register(User user)
        {
            return await _userRepository.Register(user);
        }
    }
}
