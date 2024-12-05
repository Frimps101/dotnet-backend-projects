using Microsoft.EntityFrameworkCore;
using TodoApi.Commons;
using TodoApi.Commons.Models;
using TodoApi.Data;
using TodoApi.Models;
using TodoApi.Repositories.Interfaces;

namespace TodoApi.Repositories.Implementations;

public class UserRepository:IUserRepository
{
    private readonly TodoApiDbContext _dbContext;

    public UserRepository(TodoApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public IQueryable<User> GetUsers()
    {
        return _dbContext.Users.AsNoTracking().AsQueryable();
    }

    public async Task<PagedResult<User>> FilterUsers(UsersFilter filter, CancellationToken token)
    {
        var query = GetUsers();

        if (!string.IsNullOrEmpty(filter.Search))
        {
            query = query.Where(x =>
                (!string.IsNullOrEmpty(x.Username) && x.Username.Contains(filter.Search)) ||
                (!string.IsNullOrEmpty(x.Email) && x.Email.Contains(filter.Search)));
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

    public async Task<int> AddUser(User user)
    {
      await _dbContext.AddAsync(user);
      var addRes = await _dbContext.SaveChangesAsync();
       return addRes ;
    }

    public async Task<User?> GetUserById(string id)
    {
        var user = await GetUsers().FirstOrDefaultAsync(user => id.Equals(user.Id));
        return user;
    }

    public async Task<User?> GetUserByEmail(string email)
    {
        var user = await GetUsers().FirstOrDefaultAsync(user => email.Equals(user.Email));
        return user;
    }

    public async Task<int> UpdateUser(User user)
    {
        _dbContext.Users.Update(user);
        var saveResponse = await _dbContext.SaveChangesAsync();
        return saveResponse;
    }

    public async Task<int> DeleteUser(User user)
    {
        _dbContext.Users.Remove(user);
        var deleteResponse = await _dbContext.SaveChangesAsync();
        return deleteResponse;
    }
}