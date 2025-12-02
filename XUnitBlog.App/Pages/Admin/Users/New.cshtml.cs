using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Dtos.Users;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Users;

[Authorize(Roles = "ADMIN")]
public class NewModel(UserService userService) : PageModel
{
    [BindProperty]
    public CreateUserDto CreateUser { get; set; }

    [BindProperty]
    public IFormFile Photo { get; set; }
    public Dictionary<string, string> Errors { get; set; } = [];

    public void OnGet() { }

    public async Task OnPostAsync()
    {
        try
        {
            await userService.AddAsync(CreateUser);

            Response.Redirect("/admin/users");
        }
        catch (ArgumentException e)
        {
            Errors.Add(nameof(CreateUser), e.Message);
        }
    }
}
