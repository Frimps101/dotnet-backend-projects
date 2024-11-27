namespace TodoApi.Commons.Models;
#nullable disable

public class TodosFilter:BaseFilter
{
    public string Search { get; set; }
    public DateTime Deadline { get; set; }
    public string Status { get; set; }
    public string Priority { get; set; }
}