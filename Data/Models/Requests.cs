using System;
using System.Collections.Generic;

namespace TaskManager.Data.Models
{
    public partial class Request
    {
        public Request()
        {
            Notifications = new HashSet<Notification>();
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public int TaskId { get; set; }

        public User Receiver { get; set; }
        public User Sender { get; set; }
        public Task Task { get; set; }
        public ICollection<Notification> Notifications { get; set; }
    }
}
