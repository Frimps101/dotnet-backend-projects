using TodoApi.Commons.Models;

namespace TodoApi.Models;

public class Role:BaseModel
{
    public string Name { get; set; }
    public string Description { get; set; }
    public List<User> Users { get; set; }
}