using XUnitBlog.Domain.Services;
using XUnitBlog.Test.Builders;

namespace XUnitBlog.Test.Auth;

public class JwtServiceTest
{
    [Fact]
    public void ShouldGenerateJwtToken()
    {
        var user = UserBuilder.New().Build();
        var jwtService = new JwtService("NEcyJWg3dzpiRChEMlY5Mm19O3BldnA=", "xunit-blog");

        var token = jwtService.GenerateJwtToken(user);

        Assert.NotEmpty(token);
    }
}
