using TodoApi.Commons.Models;

namespace TodoApi.Models;
#nullable disable

public class User:BaseModel
{
    public string Username { get; set; }
    public string Email { get; set; } 
    public string Role { get; set; }
    public List<Todo> Todos { get; set; }
}