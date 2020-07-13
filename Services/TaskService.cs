using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data.Repositories;
using TaskManager.Helpers.Requests.Tasks;
using TaskManager.Helpers.Responses.Tasks;

namespace TaskManager.Services
{
    public class TaskService
    {
        private TaskRepository _taskRepository;
        private UsersTaskRepository _usersTaskRepository;
        private RequestRepository _requestRepository;
        private UserRepository _userRepository;
        private NotificationRepository _notificationRepository;

        public TaskService(TaskRepository taskRepository, UsersTaskRepository usersTaskRepository, RequestRepository requestRepository, UserRepository userRepository, NotificationRepository notificationRepository)
        {
            _taskRepository = taskRepository;
            _usersTaskRepository = usersTaskRepository;
            _requestRepository = requestRepository;
            _userRepository = userRepository;
            _notificationRepository = notificationRepository;
        }

        public async Task<AddTaskResponse> Add(Data.Models.Task task, List<SingleRequest> requests, int userId)
        {
            var newTask = await _taskRepository.AddAsync(task);
            List<string> failedRequestEmails = new List<string>();
            if (newTask != null)
            {
                await _usersTaskRepository.AddAsync(new Data.Models.UsersTask
                {
                    UserId = userId,
                    TaskId = newTask.Id,
                    PrivilegeId = 1
                });

                foreach (SingleRequest request in requests)
                {
                    var user = _userRepository.GetByCondition(u => u.Email == request.Email).FirstOrDefault();
                    if (user != null)
                    {
                        var newRequest = await _requestRepository.AddAsync(new Data.Models.Request
                        {
                            SenderId = userId,
                            ReceiverId = user.Id,
                            TaskId = newTask.Id,
                            PrivilegeId = request.PrivilegeId,
                        });

                        await _notificationRepository.AddAsync(new Data.Models.Notification
                        {
                            RequestId = newRequest.Id,
                            UserId = user.Id
                        });
                    } else
                    {
                        failedRequestEmails.Add(request.Email);
                    }
                }

                return new AddTaskResponse
                {
                    Task = new Helpers.Responses.Tasks.Task
                    {
                        Title = newTask.Title,
                        Description = newTask.Description,
                        IsActive = newTask.IsActive,
                        Deadline = newTask.Deadline,
                        Notes = newTask.Notes,
                    },
                    FailedRequestEmails = failedRequestEmails
                };
            }
            else return new AddTaskResponse();
        }
    }
}
