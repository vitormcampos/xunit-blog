using XUnitBlog.Domain.Exceptions;

namespace XUnitBlog.Domain.Entities;

public class Post
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string? Content { get; private set; }
    public string? Thumbnail { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }
    public PostStatuses PostStatus { get; private set; }
    public bool Pinned { get; private set; }
    public long UserId { get; private set; }
    public User User { get; private set; }

    private Post() { }

    public Post(string title, string content, string thumbnail, long userId, bool pinned = false)
    {
        SetTitle(title);
        SetContent(content);
        SetThumbnail(thumbnail);
        CreatedAt = DateTime.UtcNow;
        PostStatus = PostStatuses.Draft;
        Pinned = pinned;

        if (userId <= 0)
        {
            throw new DomainModelException("User id is invalid");
        }

        UserId = userId;
        Pinned = pinned;
    }

    public void SetTitle(string title)
    {
        if (string.IsNullOrEmpty(title))
        {
            throw new DomainModelException("Title is required");
        }
        Title = title;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetContent(string content)
    {
        if (string.IsNullOrEmpty(content))
        {
            PostStatus = PostStatuses.Draft;
        }
        Content = content;
        UpdatedAt = DateTime.UtcNow;
    }

    public void SetThumbnail(string thumbnail)
    {
        Thumbnail = thumbnail;
        UpdatedAt = DateTime.UtcNow;
    }

    public void PinPost() => Pinned = true;

    public void UnpinPost() => Pinned = false;

    public void SetStatus(PostStatuses status) => PostStatus = status;
}
