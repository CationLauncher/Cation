using Cation.Core.Authentication;

namespace Cation.Test.Core;

public class AuthenticationTest : CationTestBase
{
    [Test]
    [Explicit]
    public async Task AuthenticateWithMsa()
    {
        var profile = await Authentication.AuthenticateWithMsaAsync(deviceCodeResult =>
        {
            Console.WriteLine(deviceCodeResult.Message);
            return Task.FromResult(0);
        });
        Assert.That(profile, Is.Not.Null);
    }
}
