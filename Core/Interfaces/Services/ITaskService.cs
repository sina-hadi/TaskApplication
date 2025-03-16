using Core.DTOs;
using Core.DTOs.Task;

namespace Core.Interfaces.Services
{
    public interface ITaskService
    {
        Task<AppResponse<List<TaskGet>>> GetAllAsync();

        Task<AppResponse<TaskGet>> GetAsync(string id);

        Task<AppResponse<string>> InsertAsync(TaskCreate task);

        Task<AppResponse> DeleteAsync(string id);

        Task<AppResponse> UpdateAsync(string id, TaskUpdate task);
    }
}
