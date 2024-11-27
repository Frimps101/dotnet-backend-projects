using TodoApi.Commons.Models;
using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

public interface ITodoRepository
{

    Task<int> AddTodo(Todo todo);
    Task<Todo?> GetById(string id);
    Task<PagedResult<Todo>>FilterTodos(TodosFilter filter, CancellationToken token=default);
    Task<int> UpdateTodo(Todo todo);
    Task<int> DeleteTodo(Todo todo);
}