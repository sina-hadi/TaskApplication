using Core.DTOs;
using Core.DTOs.Task;
using Core.Interfaces.Repositories;
using Core.Interfaces.Services;

namespace Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public async Task<AppResponse> DeleteAsync(string id)
        {
            var result = await _taskRepository.DeleteAsync(id);

            return result;
        }

        public async Task<AppResponse<List<TaskGet>>> GetAllAsync()
        {
            var result = await _taskRepository.GetAllAsync();

            return result;
        }

        public async Task<AppResponse<TaskGet>> GetAsync(string id)
        {
            var result = await _taskRepository.GetAsync(id);

            return result;
        }

        public async Task<AppResponse<string>> InsertAsync(TaskCreate task)
        {
            var taskRepo = new TaskCreateRepo
            {
                Id = Guid.NewGuid().ToString(),
                Title = task.Title,
                Description = task.Description
            };

            var result = await _taskRepository.InsertAsync(taskRepo);

            return result;
        }

        public async Task<AppResponse> UpdateAsync(string id, TaskUpdate task)
        {
            var taskRepo = new TaskUpdateRepo
            {
                Id = id,
                Title = task.Title,
                Description = task.Description,
                IsCompleted = task.IsCompleted
            };

            var result = await _taskRepository.UpdateAsync(taskRepo);

            return result;
        }
    }
}
