using TodoApi.Commons.Models;

namespace TodoApi.Models;
#nullable disable

public class User:BaseModel
{
    public string Username { get; set; } = null!;
    public string Email { get; set; } = null!;
    public List<Role> Roles { get; set; }= new();
    public string PasswordHash { get; set; }= null!;
    public List<Todo> Todos { get; set; } = new();
}