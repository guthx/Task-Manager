using System;
using System.Collections.Generic;

namespace TaskManager.Data.Models
{
    public partial class Notification
    {
        public int Id { get; set; }
        public int? TaskId { get; set; }
        public int? RequestId { get; set; }
        public bool IsViewed { get; set; }
        public int UserId { get; set; }

        public Request Request { get; set; }
        public Task Task { get; set; }
        public User User { get; set; }
    }
}
