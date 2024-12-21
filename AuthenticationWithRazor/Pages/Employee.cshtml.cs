using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationWithRazor.Pages;

[Authorize(Roles = "employee")]
public class Employee : PageModel
{
    public void OnGet()
    {
        
    }
}