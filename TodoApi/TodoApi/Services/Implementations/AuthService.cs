using Microsoft.AspNetCore.Identity;
using TodoApi.Commons.Models;
using TodoApi.Dtos;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;
using TodoApi.Services.Interfaces;

namespace TodoApi.Services.Implementations;

public class AuthService:IAuthService
{
    private readonly ILogger<AuthService> _logger;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IRoleRepository _roleRepository;
    
    public AuthService(ILogger<AuthService> logger, IUserRepository userRepository, IPasswordHasher<User> passwordHasher, IRoleRepository roleRepository)
    {
        _logger = logger;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _roleRepository = roleRepository;
    }
    public async Task<ApiResponse<CreateUserResponse>> CreateUserAsync(CreateUserRequest request)
    {
        try
        {
            _logger.LogDebug("[CreateUserAsync] about to start process to create user with request: {Request}", request);
            
            // Check if user already exists using email
            var existingUser = await _userRepository.GetUserByEmail(request.Email);
            
            if (existingUser != null)
            {
                _logger.LogWarning("[CreateUserAsync] user with email {Email} already exists", request.Email);
                return new ApiResponse<CreateUserResponse>
                (
                    Code: StatusCodes.Status400BadRequest,
                    Message: "User with email already exists"
                );
            }
            
            // Create user
            var user = new User
            {
                Username = request.Username,
                Email = request.Email
            };
            
            // Hash password
            user.PasswordHash = _passwordHasher.HashPassword(user, request.Password);
            
            // Add user roles
            // retrieve user roles
            var role = await _roleRepository.GetDefaultRoleAsync();
            
            user.Roles.Add(role);
            
            // Add user
            var result = await _userRepository.AddUser(user);
            
            if(result < 1)
            {
                _logger.LogWarning("[CreateUserAsync] user with email {Email} could not be created", request.Email);
                return new ApiResponse<CreateUserResponse>
                (
                    Code: StatusCodes.Status400BadRequest,
                    Message: "User could not be created"
                );
            }
            
            _logger.LogDebug("[CreateUserAsync] user with email {Email} created successfully", request.Email);
            return new ApiResponse<CreateUserResponse>
            (
                Code: StatusCodes.Status201Created,
                Message: "User created successfully",
                Data: new CreateUserResponse
                {
                    Username = user.Username,
                    Email = user.Email
                }
            );
            
        }
        catch (Exception e)
        {
            _logger.LogError(e, "[CreateUserAsync] error occurred while creating user with request: {Request}", request);
            return new ApiResponse<CreateUserResponse>
            (
                Code: StatusCodes.Status500InternalServerError,
                Message: "An error occurred while creating user"
            );
        }
    }
}