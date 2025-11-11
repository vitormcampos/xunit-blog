using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Users;

public class IndexModel(UserService userService) : PageModel
{
    public void OnGet() { }
}
