using XUnitBlog.Domain.Exceptions;

namespace XUnitBlog.Test.Extensions;

public static class AssertExtensions
{
    public static void WithMessage(this DomainModelException exception, string message)
    {
        Assert.Equal(message, exception.Message);
    }

    public static void WithMessage(this DomainServiceException exception, string message)
    {
        Assert.Equal(message, exception.Message);
    }

    public static async Task WithMessageAsync(
        this Task<DomainModelException> exception,
        string message
    )
    {
        Assert.Equal(message, (await exception).Message);
    }

    public static async Task WithMessageAsync(
        this Task<DomainServiceException> exception,
        string message
    )
    {
        Assert.Equal(message, (await exception).Message);
    }
}
