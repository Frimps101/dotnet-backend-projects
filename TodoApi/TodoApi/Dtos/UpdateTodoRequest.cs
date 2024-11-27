using System.ComponentModel.DataAnnotations;

namespace TodoApi.Dtos;
#nullable disable
public class UpdateTodoRequest
{
    public string Title { get; set; }
    public string Description { get; set; }
    [RegularExpression($"{Commons.CommonConstants.TodoPriority.Low}|{Commons.CommonConstants.TodoPriority.Medium}|{Commons.CommonConstants.TodoPriority.High}")]
    public string Priority { get; set; } 
    public DateTime Deadline { get; set; }
    [RegularExpression($"{Commons.CommonConstants.TodoStatus.Pending}|{Commons.CommonConstants.TodoStatus.InProgress}|{Commons.CommonConstants.TodoStatus.Completed}")]
    public string Status { get; set; } 
}