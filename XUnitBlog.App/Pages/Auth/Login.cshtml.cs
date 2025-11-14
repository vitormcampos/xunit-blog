using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Auth;

public class LoginModel(UserService userService) : PageModel
{
    [BindProperty]
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [BindProperty]
    [Required]
    public string Password { get; set; }

    public void OnGet() { }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            return Page();
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

        return RedirectToPage("/Index");
    }
}
