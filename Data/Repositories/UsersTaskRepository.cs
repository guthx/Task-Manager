using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class UsersTaskRepository : BaseRepository<Models.UsersTask>
    {
        public UsersTaskRepository(TaskManagerDBContext dbContext)
            : base(dbContext)
        {
        }

    }
}
