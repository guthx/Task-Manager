using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Helpers.Responses.Tasks
{
    public class Task
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? Deadline { get; set; }
        public string Notes { get; set; }
        public List<Task> Children { get; set; }
        public bool IsActive { get; set; }
    }
}
