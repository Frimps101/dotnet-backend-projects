using TodoApi.Commons.Models;
using TodoApi.Dtos;
using TodoApi.Models;

namespace TodoApi.Services.Interfaces;

public interface ITodoService
{
    Task<ApiResponse<Todo>> GetTodoById(string id);
    Task<ApiResponse<PagedResult<Todo>>> FilterTodos(TodosFilter filter);
    Task<ApiResponse<Todo>> CreateTodo(CreateTodoRequest request);
    Task<ApiResponse<Todo>> UpdateTodo(string id, UpdateTodoRequest request);
    Task<ApiResponse<Todo>> DeleteTodo(string id);
}