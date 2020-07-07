using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;
using BCrypt;
using System.Text;

namespace TaskManager.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(TaskManagerDBContext dbContext)
            : base(dbContext) 
        {
        }

        public async Task<User> Register(User user)
        {
            if (GetByCondition(u => u.Email == user.Email).FirstOrDefault() != null)
            {
                return null;
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(user.Password, 13);
            user.Password = hashedPassword;
            return await AddAsync(user);
        }
    }
}
