using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

public interface IRoleRepository
{
    Task<int> AddRole(Role role);
    Task<Role?> GetDefaultRoleAsync();
}