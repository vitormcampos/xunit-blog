using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.App.Extensions;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Posts;

[Authorize]
public class IndexModel(PostService postService) : PageModel
{
    [BindProperty]
    public IList<Post> Posts { get; set; }

    public async Task OnGet()
    {
        Posts = User.IsInRole("Admin")
            ? await postService.GetAll()
            : await postService.GetAllByUser(User.GetId());
    }
}
