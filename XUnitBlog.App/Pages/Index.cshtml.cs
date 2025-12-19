using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Entities;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages;

public class IndexModel(PostService postService) : PageModel
{
    public IList<Post> PinnedPosts { get; set; }

    public IList<Post> LatestPosts { get; set; }

    public async Task OnGetAsync()
    {
        PinnedPosts = await postService.GetAllPinnedPosts();
        LatestPosts = await postService.GetAll();
    }
}
