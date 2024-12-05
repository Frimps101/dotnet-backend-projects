using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories.Implementations;

public class RoleRepository:IRoleRepository
{
    private readonly TodoApiDbContext _dbContext;
    
    public RoleRepository(TodoApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<int> AddRole(Role role)
    {
        await _dbContext.Roles.AddAsync(role);
        var addRes = await _dbContext.SaveChangesAsync();
        return addRes ;
    }

    public async Task<Role?> GetDefaultRoleAsync()
    {
        return await _dbContext.Roles.FirstOrDefaultAsync(r => r.Name == "User");
    }
}