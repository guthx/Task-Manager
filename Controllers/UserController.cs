using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Helpers.Requests.Users;
using TaskManager.Helpers.Responses.Users;
using TaskManager.Services;

namespace TaskManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserService _userService;
        public UserController(UserService userService)
        {
            _userService = userService;
        }
        // GET api/values
        [HttpPost]
        public async Task<StatusCodeResult> Register(RegisterRequest request)
        {
            try
            {
                var user = await _userService.Register(new Data.Models.User
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Password = request.Password
                });
                if (user != null)
                {
                    return StatusCode(200);
                }
                else
                {
                    return StatusCode(400);
                }
            } catch (Exception ex)
            {
                return StatusCode(500);
            }
            
            
        }

        [HttpPost("login")]
        public ActionResult<LoginResponse> Login(LoginRequest request)
        {
            try
            {
                var token = _userService.Login(request.Email, request.Password);
                if (token == null)
                {
                    return StatusCode(400);
                }
                else
                {
                    return new LoginResponse { Jwt = token };
                }
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }
    }
}
