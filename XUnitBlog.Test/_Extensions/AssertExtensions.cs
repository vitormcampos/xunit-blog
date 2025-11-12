namespace XUnitBlog.Test.Extensions;

public static class AssertExtensions
{
    public static void WithMessage(this Exception exception, string message)
    {
        Assert.Equal(message, exception.Message);
    }

    public static async Task WithMessageAsync(this Task<Exception> exception, string message)
    {
        Assert.Equal(message, (await exception).Message);
    }

    public static async Task WithMessageAsync(
        this Task<ArgumentException> exception,
        string message
    )
    {
        Assert.Equal(message, (await exception).Message);
    }
}
