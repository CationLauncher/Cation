using Cation.Core.Microsoft;

namespace Cation.Test.Core;

public class AuthenticationTest : CationTestBase
{
    [Test]
    [Explicit]
    public async Task GetAccessTokenTest()
    {
        var token = await Authentication.GetMicrosoftAccessTokenAsync(deviceCodeResult =>
        {
            Console.WriteLine(deviceCodeResult.Message);
            return Task.FromResult(0);
        });
        Assert.That(token, Is.Not.Null);
    }
}
