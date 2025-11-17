using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Dtos.Post;

public class UpdatePostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Thumbnail { get; set; }

    internal Post MapToPost(long userId)
    {
        return new Post(Title, Content, Thumbnail, userId);
    }
}
