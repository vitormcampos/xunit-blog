using XUnitBlog.Domain.Services;
using XUnitBlog.Test.Extensions;

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
    [InlineData("")]
    [InlineData(null)]
    public void ShouldThrowsIfInputStringIsInvalid(string input)
    {
        var hashService = new HashService();

        void assertAction()
        {
            var output = hashService.CreateHash(input);
        }

        Assert.Throws<ArgumentException>(assertAction).WithMessage("The input cannot be empty");
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
