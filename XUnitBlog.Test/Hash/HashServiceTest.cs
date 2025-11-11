using XUnitBlog.Domain.Services;

namespace XUnitBlog.Test.Hash;

public class HashServiceTest
{
    [Fact]
    public void ShouldProduceHash()
    {
        var input = "olamundo";

        var hashService = new HashService();

        var output = hashService.CreateHash(input);

        Assert.NotEmpty(output);
    }

    [Theory]
    [InlineData("olamundo")]
    [InlineData("123456")]
    public void ShouldVerifyHashMatchesExpected(string input)
    {
        var hashService = new HashService();

        var hash = hashService.CreateHash(input);

        var checkHash = hashService.CompareHash(input, hash);

        Assert.True(checkHash);
    }
}
