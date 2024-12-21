using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationWithRazor.Pages;

[Authorize(Roles = "admin")]
public class Admin : PageModel
{
    public void OnGet()
    {
        
    }
}