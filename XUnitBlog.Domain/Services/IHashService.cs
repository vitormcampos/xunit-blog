namespace XUnitBlog.Domain.Services;

public interface IHashService
{
    string CreateHash(string input);
    bool CompareHash(string input, string hash);
}
