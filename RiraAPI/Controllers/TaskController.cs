using Application.Services;
using Core.DTOs;
using Core.DTOs.Task;
using Core.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace RiraAPI.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromRoute] string id)
        => await HandleServiceResponse(() => _taskService.GetAsync(id));

        [HttpGet]
        public async Task<IActionResult> GetAll()
            => await HandleServiceResponse(() => _taskService.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaskCreate product)
            => await HandleServiceResponse(() => _taskService.InsertAsync(product), onSuccess: 201);

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromRoute] string id, [FromBody] TaskUpdate product)
            => await HandleServiceResponse(() => _taskService.UpdateAsync(id, product));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] string id)
            => await HandleServiceResponse(() => _taskService.DeleteAsync(id));

        private async Task<IActionResult> HandleServiceResponse(Func<Task<AppResponse>> serviceCall, int onSuccess = 200)
        {
            try
            {
                var result = await serviceCall();
                return result.Status switch
                {
                    0 => StatusCode(onSuccess, result),
                    -1 => NoContent(),
                    -2 => Conflict(),
                    _ => UnprocessableEntity(result)
                };
            }
            catch (Exception ex)
            {
                // Log the exception (use ILogger or a logging framework)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }

        private async Task<IActionResult> HandleServiceResponse<T>(Func<Task<AppResponse<T>>> serviceCall, int onSuccess = 200)
        {
            try
            {
                var result = await serviceCall();
                return result.Status switch
                {
                    0 => StatusCode(onSuccess, result),
                    -1 => NoContent(),
                    -2 => Conflict(result),
                    _ => UnprocessableEntity(result)
                };
            }
            catch (Exception ex)
            {
                // Log the exception (use ILogger or a logging framework)
                Console.WriteLine(ex.Message);
                return StatusCode(500, "An unexpected error occurred. Please try again later.");
            }
        }
    }
}
