namespace XUnitBlog.Domain.Exceptions;

public class DomainServiceException(string message) : ArgumentException(message) { }
