using System;
using System.Collections.Generic;

namespace TaskManager.Data.Models
{
    public partial class UsersTask
    {
        public int UserId { get; set; }
        public int TaskId { get; set; }
        public int PrivilegeId { get; set; }

        public Privilege Privilege { get; set; }
    }
}