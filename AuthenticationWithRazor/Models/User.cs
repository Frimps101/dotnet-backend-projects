using Microsoft.AspNetCore.Identity;

namespace AuthenticationWithRazor.Models;

public class User:IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime CreatedAt { get; set; } 
}