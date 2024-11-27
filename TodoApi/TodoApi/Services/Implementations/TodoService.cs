using Newtonsoft.Json;
using TodoApi.Commons.Models;
using TodoApi.Dtos;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services.Implementations;

public class TodoService:ITodoService
{
    private readonly ILogger<TodoService> _logger;
    private readonly ITodoRepository _todoRepository;
    public TodoService(ILogger<TodoService> logger, ITodoRepository todoRepository)
    {
        _logger = logger;
        _todoRepository = todoRepository;
    }
    public async Task<ApiResponse<Todo>> GetTodoById(string id)
    {
        try
        {
            _logger.LogDebug("[GetTodoById] received request to retrieve a to-do by id");

            var todo = await _todoRepository.GetById(id);

            if (todo == null)
            {
                _logger.LogWarning("[GetTodoById] to-do doesnt exist");
                return new ApiResponse<Todo>
                (
                    Code: StatusCodes.Status404NotFound,
                    Message: "to-do doesnt exist",
                    Data : null
                );
                
            }
            
            _logger.LogInformation("[GetTodoById] to-do retrieved successfully");
            return new ApiResponse<Todo>
            (
                Code: StatusCodes.Status200OK,
                Message: "to-do retrieved successfully",
                Data : todo
            );
        }
        catch (Exception e)
        {
           _logger.LogError(e, "[GetTodoById] an error occurred retrieving to-do");
              return new ApiResponse<Todo>
              (
                Code: StatusCodes.Status500InternalServerError,
                Message: "an error occurred retrieving to-do",
                Data : null
              );
        }
    }

    public async Task<ApiResponse<PagedResult<Todo>>> FilterTodos(TodosFilter filter)
    {
        try
        {
            _logger.LogDebug("[FilterTodos] received request to filter to-dos. Filter: {Filter}", JsonConvert.SerializeObject(filter));
            var todos = await _todoRepository.FilterTodos(filter);
            
            if (!todos.Results.Any())
            {
                _logger.LogWarning("[FilterTodos] no to-dos found");
                return new ApiResponse<PagedResult<Todo>>
                (
                    Code: StatusCodes.Status404NotFound,
                    Message: "no to-dos found",
                    Data : null
                );
            }
            
            _logger.LogDebug("[FilterTodos] to-dos filtered successfully");
            return new ApiResponse<PagedResult<Todo>>
            (
                Code: StatusCodes.Status200OK,
                Message: "to-dos filtered successfully",
                Data : todos
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[FilterTodos] an error occurred filtering to-dos");
            return new ApiResponse<PagedResult<Todo>>
            (
                Code: StatusCodes.Status500InternalServerError,
                Message: "an error occurred filtering to-dos",
                Data : null
            );
        }
    }

    public async Task<ApiResponse<Todo>> CreateTodo(CreateTodoRequest request)
    {
        try
        {
            _logger.LogDebug("[CreateTodo] received request to create a to-do");
            
            var todo = new Todo
            {
                Title = request.Title,
                Description = request.Description,
                Status = request.Status,
                Priority = request.Priority,
                Deadline = request.Deadline,
                CreatedAt = DateTime.Now,
            };
            
            var addedTodo = await _todoRepository.AddTodo(todo);
            
            if (addedTodo < 1)
            {
                _logger.LogWarning("[CreateTodo] could not create to-do");
                return new ApiResponse<Todo>
                (
                    Code: StatusCodes.Status500InternalServerError,
                    Message: "could not create to-do",
                    Data : null
                );
            }
            
            _logger.LogInformation("[CreateTodo] to-do created successfully");
            return new ApiResponse<Todo>
            (
                Code: StatusCodes.Status200OK,
                Message: "to-do created successfully",
                Data : todo
            );
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[CreateTodo] an error occurred creating to-do");
            return new ApiResponse<Todo>
            (
                Code: StatusCodes.Status500InternalServerError,
                Message: "an error occurred creating to-do",
                Data : null
            );
        }
    }

    public async Task<ApiResponse<Todo>> UpdateTodo(string id, UpdateTodoRequest request)
    {
        try
        {
            _logger.LogDebug("[UpdateTodo] received request to update a to-do by id: {Id}", id);
            
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
            {
                _logger.LogDebug("[UpdateTodo] to-do doesnt exist");
                return new ApiResponse<Todo>
                (
                    Code: StatusCodes.Status404NotFound,
                    Message: "to-do doesnt exist",
                    Data : null
                );
            }
            
            todo.Title = request.Title;
            todo.Description = request.Description;
            todo.Status = request.Status;
            todo.Priority = request.Priority;
            todo.Deadline = request.Deadline;
            todo.UpdatedAt = DateTime.Now;
            
            
            var updatedTodoResponse = await _todoRepository.UpdateTodo(todo);

            if (updatedTodoResponse < 1)
            {
                _logger.LogDebug("[UpdateTodo] could not update to-do");
                return new ApiResponse<Todo>
                (
                    Code: StatusCodes.Status500InternalServerError,
                    Message: "could not update to-do",
                    Data : null
                );
            }
            
            _logger.LogDebug("[UpdateTodo] to-do updated successfully with request: {Request}", todo);
            return new ApiResponse<Todo>
            (
                Code: StatusCodes.Status200OK,
                Message: "to-do updated successfully",
                Data : todo
            );
        }
        catch (Exception e)
        {
           _logger.LogError(e, "[UpdateTodo] an error occurred updating to-do. Error: {Error}", e.Message);
              return new ApiResponse<Todo>
              (
                Code: StatusCodes.Status500InternalServerError,
                Message: "an error occurred updating to-do",
                Data : null
              );
        }
    }

    public async Task<ApiResponse<Todo>> DeleteTodo(string id)
    {
        try
        {
            _logger.LogDebug("[DeleteTodo] received request to delete a to-do by id: {Id}", id);
            
            var todo = await _todoRepository.GetById(id);
            if (todo == null)
            {
                _logger.LogDebug("[DeleteTodo] to-do doesnt exist");
                return new ApiResponse<Todo>
                (
                    Code: StatusCodes.Status404NotFound,
                    Message: "to-do doesnt exist",
                    Data : null
                );
            }
            
            var deletedTodoResponse = await _todoRepository.DeleteTodo(todo);
            if (deletedTodoResponse < 1)
            {
                _logger.LogDebug("[DeleteTodo] could not delete to-do");
                return new ApiResponse<Todo>
                (
                    Code: StatusCodes.Status500InternalServerError,
                    Message: "could not delete to-do",
                    Data : null
                );
            }
            
            _logger.LogDebug("[DeleteTodo] to-do deleted successfully");
            return new ApiResponse<Todo>
            (
                Code: StatusCodes.Status200OK,
                Message: "to-do deleted successfully",
                Data : todo
            );
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[DeleteTodo] an error occurred deleting to-do. Error: {Error}", e.Message);
            return new ApiResponse<Todo>
            (
                Code: StatusCodes.Status500InternalServerError,
                Message: "an error occurred deleting to-do",
                Data : null
            );
        }
    }
}