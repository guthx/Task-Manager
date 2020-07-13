using System;
using System.Collections.Generic;

namespace TaskManager.Data.Models
{
    public partial class Privilege
    {
        public Privilege()
        {
            UsersTasks = new HashSet<UsersTask>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<UsersTask> UsersTasks { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
