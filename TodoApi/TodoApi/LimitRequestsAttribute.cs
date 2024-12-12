namespace TodoApi;

[AttributeUsage(AttributeTargets.Method)]
public class LimitRequestsAttribute: Attribute
{
    public int TimeWindow { get; set; }
    public int MaxRequests { get; set; }
}