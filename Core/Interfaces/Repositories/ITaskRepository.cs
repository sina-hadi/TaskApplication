using Core.DTOs;
using Core.DTOs.Task;

namespace Core.Interfaces.Repositories
{
    public interface ITaskRepository
    {
        Task<AppResponse<List<TaskGet>>> GetAllAsync();

        Task<AppResponse<TaskGet>> GetAsync(string id);

        Task<AppResponse<string>> InsertAsync(TaskCreateRepo task);

        Task<AppResponse> DeleteAsync(string id);

        Task<AppResponse> UpdateAsync(TaskUpdateRepo task);
    }
}
