using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Users;

[Authorize(Roles = "ADMIN")]
public class IndexModel(UserService userService) : PageModel
{
    public IList<User> Users { get; private set; }

    public async Task OnGetAsync()
    {
        Users = await userService.GetAll();
    }
}
