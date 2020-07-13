using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;
using TaskManager.Helpers.Responses.Tasks;

namespace TaskManager.Data.Repositories
{
    public class TaskRepository : BaseRepository<Models.Task>
    {
        public TaskRepository(TaskManagerDBContext dbContext)
            : base(dbContext)
        { 
        }

    }
}
