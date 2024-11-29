using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Swashbuckle.AspNetCore.Annotations;
using TodoApi.Commons.Models;
using TodoApi.Dtos;
using TodoApi.Models;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TodoController:ControllerBase
{
    private readonly ITodoService _todoService;
    private readonly IMemoryCache _memoryCache;
    public TodoController(ITodoService todoService, IMemoryCache memoryCache)
    {
        _todoService = todoService;
        _memoryCache = memoryCache;
    }
    
    /// <summary>
    /// create a to-do
    /// param id
    /// </summary>
    /// <returns></returns>
    [HttpPost("add-todo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Todo>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Todo>))]
    [SwaggerOperation(nameof(AddTodo), OperationId = nameof(AddTodo))]
    public async Task<IActionResult> AddTodo([FromBody] CreateTodoRequest createTodoRequest)
    {
        var result = await _todoService.CreateTodo(createTodoRequest);
        return StatusCode(result.Code, result);
    }
    
    /// <summary>
    /// retrieve paginated list of todos
    /// filter todos
    /// param id
    /// </summary>
    /// <returns></returns>
    [HttpGet("filter-todos")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Todo>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Todo>))]
    [SwaggerOperation(nameof(FilterTodos), OperationId = nameof(FilterTodos))]
    public async Task<IActionResult> FilterTodos([FromQuery] TodosFilter filter)
    {
        if(_memoryCache.TryGetValue("allTodos", out var cachedResult))
        {
            return Ok(cachedResult);
        }
        
        var result = await _todoService.FilterTodos(filter);
        
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(5));
        _memoryCache.Set("allTodos", result, cacheEntryOptions);
        
        return StatusCode(result.Code, result);
    }
    
    /// <summary>
    /// retrieve a to-do by id
    /// param id
    /// </summary>
    /// <returns></returns>
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Todo>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Todo>))]
    [SwaggerOperation(nameof(GetTodoById), OperationId = nameof(GetTodoById))]
    public async Task<IActionResult> GetTodoById([FromRoute] string id)
    {
        var result = await _todoService.GetTodoById(id);
        return StatusCode(result.Code, result);
    }
    
    /// <summary>
    /// update a to-do by id
    /// param id
    /// </summary>
    /// <returns></returns>
    [HttpPatch("{id}/update-todo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Todo>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Todo>))]
    [SwaggerOperation(nameof(Update), OperationId = nameof(Update))]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateTodoRequest request)
    {
        var result = await _todoService.UpdateTodo(id, request);
        return StatusCode(result.Code, result);
    }
    
    /// <summary>
    /// delete a to-do by id
    /// param id
    /// </summary>
    /// <returns></returns>
    [HttpDelete("{id}/delete-todo")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ApiResponse<Todo>))]
    [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ApiResponse<Todo>))]
    [SwaggerOperation(nameof(DeleteTodo), OperationId = nameof(DeleteTodo))]
    public async Task<IActionResult> DeleteTodo([FromRoute] string id)
    {
        var result = await _todoService.DeleteTodo(id);
        return StatusCode(result.Code, result);
    }
}