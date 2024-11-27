using TodoApi.Commons.Models;
namespace TodoApi.Models;
#nullable disable
public class Todo : BaseModel
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Priority { get; set; } 
    public DateTime Deadline { get; set; }
    public string Status { get; set; } 
}