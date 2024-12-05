using Microsoft.AspNetCore.Mvc;
using TodoApi.Dtos;
using TodoApi.Services.Interfaces;

namespace TodoApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController:ControllerBase
{
    private readonly IAuthService _authService;
    
    public AuthenticationController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [HttpPost("signup")]
    public async Task<IActionResult> AddUser([FromBody] CreateUserRequest request)
    {
        var result = await _authService.CreateUserAsync(request);
        return StatusCode(result.Code, result);
    }
}