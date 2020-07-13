using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Helpers.Requests.Users
{
    public class RegisterRequest
    {
        [MaxLength(100)]
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [MaxLength(60)]
        [Required]
        public string Password { get; set; }
        [MaxLength(100)]
        [Required]
        public string FirstName { get; set; }
        [MaxLength(100)]
        [Required]
        public string LastName { get; set; }

    }
}
