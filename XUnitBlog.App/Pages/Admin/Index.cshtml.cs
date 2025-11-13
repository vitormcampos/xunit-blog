using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace XUnitBlog.App.Pages.Admin;

[Authorize(Roles = "ADMIN")]
public class IndexModel : PageModel
{
    public void OnGet() { }
}
