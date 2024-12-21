using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AuthenticationWithRazor.Pages;
[Authorize]
public class User : PageModel
{
    private readonly UserManager<Models.User> _userManager;
    public Models.User? appUser;
    
    public User(UserManager<Models.User> userManager)
    {
        _userManager = userManager;
    }
    public void OnGet()
    {
        var task = _userManager.GetUserAsync(User);
        task.Wait();
        appUser = task.Result;
    }
}