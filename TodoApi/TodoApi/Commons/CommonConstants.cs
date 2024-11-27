namespace TodoApi.Commons;

public static class CommonConstants
{
    public static class Roles
    {
        public const string Admin = "Admin";
        public const string User = "User";
    }
    
    public static class TodoStatus
    {
        public const string Pending = "Pending";
        public const string InProgress = "InProgress";
        public const string Completed = "Completed";
    }
    
    public static class TodoPriority
    {
        public const string Low = "Low";
        public const string Medium = "Medium";
        public const string High = "High";
    }
}