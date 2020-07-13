using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Helpers.Requests.Tasks
{
    public class AddTaskRequest
    {
        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime? Deadline { get; set; }
        public List<SingleRequest> Requests { get; set; }
    }
}
