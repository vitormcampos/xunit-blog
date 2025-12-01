namespace XUnitBlog.Domain.Exceptions;

public class DomainServiceException : ArgumentException
{
    public string? PropertyName { get; }

    public DomainServiceException(string? message)
        : base(message) { }

    public DomainServiceException(string? message, string? propertyName)
        : base(message)
    {
        PropertyName = propertyName;
    }
}
