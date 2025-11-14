namespace XUnitBlog.Domain.Exceptions;

public class DomainModelException(string message) : ArgumentException(message) { }
