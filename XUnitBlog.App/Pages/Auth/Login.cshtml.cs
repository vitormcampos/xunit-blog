using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Auth;

public class LoginModel(UserService userService) : PageModel
{
    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Password { get; set; }

    public void OnGet() { }

    public async Task OnPost()
    {
        if (string.IsNullOrEmpty(Email))
        {
            return;
        }
        if (string.IsNullOrEmpty(Password))
        {
            return;
        }

        var token = await userService.Authenticate(Email, Password);

        Response.Cookies.Append(
            "XUnitBlog_Token",
            token,
            new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddHours(1),
            }
        );
    }
}
