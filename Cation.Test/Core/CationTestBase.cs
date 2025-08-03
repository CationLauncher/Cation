namespace Cation.Test.Core;

public abstract class CationTestBase
{
    [SetUp]
    public void ServicesSetup()
    {
        App.ConfigureServices();
    }
}
