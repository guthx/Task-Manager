using System;
using System.Collections.Generic;
using System.Linq;

namespace TaskManager.Helpers.Responses.Tasks
{
    public class AddTaskResponse
    {
        public Task Task { get; set; }
        public List<string> FailedRequestEmails { get; set; }
    }
}
