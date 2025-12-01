using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Dtos.Posts;

public class UpdatePostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Thumbnail { get; set; }
    public bool Pinned { get; set; }
    public PostStatuses PostStatus { get; set; }

    internal Post MapToPost(long userId)
    {
        var post = new Post(Title, Content, Thumbnail, userId, Pinned, PostStatus);

        return post;
    }
}
