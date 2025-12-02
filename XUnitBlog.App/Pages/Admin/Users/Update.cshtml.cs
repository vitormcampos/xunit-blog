using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Dtos.Users;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Users;

[Authorize(Roles = "ADMIN")]
public class UpdateModel(UserService userService) : PageModel
{
    [BindProperty]
    public UpdateUserDto UpdateUserDto { get; set; }

    public Dictionary<string, string> Errors { get; set; } = [];

    public async Task<IActionResult> OnGetAsync(int id)
    {
        var targetUser = await userService.GetById(id);

        if (targetUser == null)
        {
            return Page();
        }

        UpdateUserDto = new()
        {
            FirstName = targetUser.FirstName,
            LastName = targetUser.LastName,
            Photo = targetUser.Photo,
        };

        return Page();
    }

    public async Task OnPostAsync(int id)
    {
        try
        {
            var result = await userService.UpdateAsync(id, UpdateUserDto);
            Response.Redirect("/Admin/Users/Index");
        }
        catch (ArgumentException e)
        {
            Errors.Add(nameof(UpdateUserDto), e.Message);
        }
    }
}
