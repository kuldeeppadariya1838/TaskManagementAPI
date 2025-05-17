using TaskManagementAPI.DTOs.Tasks;

namespace TaskManagementAPI.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<TaskDto>> GetAllTasks(int userId);
        Task<TaskDto> GetTaskById(int id, int userId);
        Task<TaskDto> CreateTask(CreateTaskDto createTaskDto, int userId);
        Task<bool> UpdateTask(int id, UpdateTaskDto updateTaskDto, int userId);
        Task<bool> DeleteTask(int id, int userId);
    }
}
