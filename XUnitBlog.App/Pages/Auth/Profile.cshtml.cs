using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XUnitBlog.App.Pages.Auth;

[Authorize]
public class ProfileModel : PageModel
{
    public void OnGet() { }
}
