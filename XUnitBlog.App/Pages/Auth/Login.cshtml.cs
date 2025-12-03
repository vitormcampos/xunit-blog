using System.ComponentModel.DataAnnotations;
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

    public Dictionary<string, string> Errors { get; set; } = [];

    public void OnGet() { }

    public async Task OnPostAsync()
    {
        try
        {
            if (string.IsNullOrEmpty(Email))
            {
                throw new ArgumentException("O campo e-mail é obrigatório.");
            }
            if (string.IsNullOrEmpty(Password))
            {
                throw new ArgumentException("O campo senha é obrigatório.");
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

            Response.Redirect("/Index");
        }
        catch (ArgumentException e)
        {
            Errors.Add("Login", e.Message);
        }
    }
}
