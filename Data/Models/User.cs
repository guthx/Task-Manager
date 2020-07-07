using System;
using System.Collections.Generic;

namespace TaskManager.Data.Models
{
    public partial class User
    {
        public User()
        {
            RequestsReceiver = new HashSet<Request>();
            RequestsSender = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<Request> RequestsReceiver { get; set; }
        public ICollection<Request> RequestsSender { get; set; }
    }
}
