using Microsoft.EntityFrameworkCore;
using TodoApi.Commons;
using TodoApi.Commons.Models;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories.Implementations;

public class TodoRepository : ITodoRepository
{
    private readonly TodoApiDbContext _dbContext;

    public TodoRepository(TodoApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    private IQueryable<Todo> GetTodos()
    {
        return _dbContext.Todos.AsNoTracking().AsQueryable();
    }

    public async Task<int> AddTodo(Todo todo)
    {
        await _dbContext.Todos.AddAsync(todo);
        var addResponse = await _dbContext.SaveChangesAsync();

        return addResponse;
    }

    public async Task<Todo?> GetById(string id)
    {
        var todo = await _dbContext.Todos.FirstOrDefaultAsync(todo => todo.Id.Equals(id));
        
        return todo;
    }

    public async Task<PagedResult<Todo>> FilterTodos(TodosFilter filter, CancellationToken token=default)
    {
        var query = GetTodos();

        if (!string.IsNullOrEmpty(filter.Search))
        {
            query = query.Where(x =>
                x.Title.ToLower().Contains(filter.Search) ||
                x.Description.ToLower().Contains(filter.Search));   
        }
        
        if (!string.IsNullOrEmpty(filter.Status))
        {
            query = query.Where(x => x.Status.ToLower().Contains(filter.Status.ToLower()));
        }
        
        if (!string.IsNullOrEmpty(filter.Priority))
        {
            query = query.Where(x => x.Priority.ToLower().Contains(filter.Priority.ToLower()));
        }
        
        if (filter.Deadline != DateTime.MinValue)
        {
            query = query.Where(x =>
                (x.Deadline.Date == filter.Deadline.Date));
        }
        
        var response = await query
            .OrderByDescending(d => d.CreatedAt)
            .Skip(Utils.GetOffset(filter.PageIndex, filter.PageSize))
            .Take(filter.PageSize)
            .ToListAsync(token);

        var totalCount = await query
            .AsNoTracking()
            .LongCountAsync(token);

        var responseRes = response.ToPagedResult(filter.PageIndex, filter.PageSize, totalCount);
        return responseRes;
    }

    public async Task<int> UpdateTodo(Todo todo)
    {
        _dbContext.Todos.Update(todo);
        var updateResponse = await _dbContext.SaveChangesAsync();
        return updateResponse;
    }

    public async Task<int> DeleteTodo(Todo todo)
    {
        _dbContext.Todos.Remove(todo);
        var deleteResponse = await _dbContext.SaveChangesAsync();

        return deleteResponse;
    }
    
}