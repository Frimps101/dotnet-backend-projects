using TodoApi.Commons.Models;
using TodoApi.Dtos;

namespace TodoApi.Services.Interfaces;

public interface IAuthService
{
    Task<ApiResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request);
}