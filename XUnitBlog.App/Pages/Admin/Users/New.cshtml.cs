using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Dtos;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Users;

public class NewModel(UserService userService) : PageModel
{
    [BindProperty]
    public string FirstName { get; set; }

    [BindProperty]
    public string LastName { get; set; }

    [BindProperty]
    public string Email { get; set; }

    [BindProperty]
    public string Role { get; set; }

    [BindProperty]
    public string Password { get; set; }

    [BindProperty]
    public string ConfirmPassword { get; set; }

    [BindProperty]
    public string UserName { get; set; }

    [BindProperty]
    public IFormFile Photo { get; set; }

    public void OnGet() { }

    public async Task OnPostAsync()
    {
        var userDto = new CreateUserDto
        {
            FirstName = FirstName,
            LastName = LastName,
            Email = Email,
            Password = Password,
            ConfirmPassword = ConfirmPassword,
            Role = Role,
            UserName = UserName,
        };

        await userService.AddAsync(userDto);

        Response.Redirect("/admin/users");
    }
}
