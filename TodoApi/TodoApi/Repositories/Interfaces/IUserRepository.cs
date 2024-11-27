using TodoApi.Commons.Models;
using TodoApi.Models;

namespace TodoApi.Repositories.Interfaces;

interface IUserRepository
{
    Task<PagedResult<User>> FilterUsers(UsersFilter filter, CancellationToken token);
    Task<int> AddUser(User user);
    Task<User?> GetUserById(string id);
    Task<int> UpdateUser(User user);
    Task<int> DeleteUser(User user);
}