using System;
using System.Collections.Generic;

namespace TaskManager.Data.Models
{
    public partial class Task
    {
        public Task()
        {
            Children = new HashSet<Task>();
            Notifications = new HashSet<Notification>();
            Requests = new HashSet<Request>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public int? ParentId { get; set; }

        public Task Parent { get; set; }
        public ICollection<Task> Children { get; set; }
        public ICollection<Notification> Notifications { get; set; }
        public ICollection<Request> Requests { get; set; }
    }
}
