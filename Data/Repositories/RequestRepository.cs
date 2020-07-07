using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class RequestRepository : BaseRepository<Request>
    {
        public RequestRepository(TaskManagerDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
