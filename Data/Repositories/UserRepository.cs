using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;
using BCrypt;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using TaskManager.Helpers;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace TaskManager.Data.Repositories
{
    public class UserRepository : BaseRepository<User>
    {
        private TokenManagement _tokenManagement;
        public UserRepository(TaskManagerDBContext dbContext, IOptions<TokenManagement> tokenManagement)
            : base(dbContext) 
        {
            _tokenManagement = tokenManagement.Value;
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

        public string Login(string email, string password)
        {
            var user = GetByCondition(u => u.Email == email).FirstOrDefault();
            bool verified = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (user == null || !verified)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_tokenManagement.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("Id", user.Id.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
