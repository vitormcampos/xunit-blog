using XUnitBlog.Domain.Entities;

namespace XUnitBlog.Domain.Dtos.Posts;

public class CreatePostDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Thumbnail { get; set; }
    public long UserId { get; set; }

    public Post MapToPost()
    {
        return new Post(Title, Content, Thumbnail, UserId);
    }
}
