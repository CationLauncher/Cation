using Cation.Core.Microsoft;

namespace Cation.Test.Core;

public class AuthenticationTest
{
    [Test]
    [Explicit]
    public async Task GetAccessTokenTest()
    {
        var token = await Authentication.GetAccessTokenAsync();
        Assert.That(token, Is.Not.Null);
    }
}
