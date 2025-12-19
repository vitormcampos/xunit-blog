using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XUnitBlog.App.Pages.Auth;

public class LogoutModel : PageModel
{
    public IActionResult OnGet()
    {
        Response.Cookies.Delete("XUnitBlog_Token");

        return RedirectToPage("/Auth/Login");
    }
}
