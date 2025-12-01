using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using XUnitBlog.App.Extensions;
using XUnitBlog.Domain.Dtos.Posts;
using XUnitBlog.Domain.Exceptions;
using XUnitBlog.Domain.Services;

namespace XUnitBlog.App.Pages.Admin.Posts;

[Authorize]
public class UpdateModel(PostService postService, UserService userService, FileService fileService)
    : PageModel
{
    [BindProperty]
    public UpdatePostDto UpdatePostDto { get; set; }

    [BindProperty]
    public IFormFile? Thumbnail { get; set; }

    public Dictionary<string, string> Errors { get; set; } = new();

    public async Task<IActionResult> OnGetAsync(long id)
    {
        var post = await postService.GetById(id);
        if (post is null)
        {
            return RedirectToPage("/NotFound");
        }

        UpdatePostDto = new UpdatePostDto
        {
            Title = post.Title,
            Content = post.Content,
            Pinned = post.Pinned,
            PostStatus = post.PostStatus,
            Thumbnail = post.Thumbnail,
        };

        return Page();
    }

    public async Task<IActionResult> OnPostAsync(long id)
    {
        try
        {
            if (Thumbnail is not null)
            {
                var thumbnailPath = await fileService.UploadAsync(Thumbnail);
                UpdatePostDto.Thumbnail = thumbnailPath;
            }

            var user = await userService.GetById(User.GetId());
            await postService.ValidateUserCanUpdatePinnedFlag(user, UpdatePostDto, id);

            await postService.UpdateAsync(id, UpdatePostDto);

            return RedirectToPage("/Admin/Posts/Index");
        }
        catch (DomainServiceException e)
        {
            Errors.Add(e.PropertyName, e.Message);
        }

        return Page();
    }
}
