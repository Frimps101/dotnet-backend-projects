using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationWithRazor.Pages;

[Authorize(Roles = "staff")]
public class Staff : PageModel
{
    public void OnGet()
    {
        
    }
}