using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.Domain.Dtos.Posts;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Posts;

[Authorize]
public class NewModel(PostService postService, FileService fileService) : PageModel
{
    [BindProperty]
    public CreatePostDto CreatePostDto { get; set; }

    [BindProperty]
    public IFormFile Thumbnail { get; set; }

    public Dictionary<string, string> Errors { get; set; } = [];

    public void OnGet() { }

    public async Task OnPostAsync()
    {
        try
        {
            if (Thumbnail is not null)
            {
                var thumbnailPath = await fileService.UploadAsync(Thumbnail);
                CreatePostDto.Thumbnail = thumbnailPath;
            }

            await postService.AddAsync(CreatePostDto);

            Response.Redirect("/Admin/Posts");
        }
        catch (ArgumentException e)
        {
            Errors.Add(nameof(CreatePostDto), e.Message);
        }
    }
}
