using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Models;

namespace TaskManager.Data.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>
    {
        public NotificationRepository(TaskManagerDBContext dbContext)
            : base(dbContext)
        {
        }
    }
}
