using Cation.Core.Java;

namespace Cation.Test.Core;

public class JavaManagerTest : CationTestBase
{
    [Test]
    public void FindJavaPath()
    {
        var result = JavaManager.FindJavaPath();
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void GetJavaList()
    {
        var result = JavaManager.GetJavaList();
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void IsCommercialOracle()
    {
        var oracle8U202 = new JavaVersion("1.8.0_202", "", "Oracle Corporation", "x86_64");
        Assert.That(oracle8U202.IsCommercialOracle, Is.False);

        var oracle8U211 = new JavaVersion("1.8.0_211", "", "Oracle Corporation", "x86_64");
        Assert.That(oracle8U211.IsCommercialOracle, Is.True);

        var oracle21 = new JavaVersion("21.0.0", "", "Oracle Corporation", "x86_64");
        Assert.That(oracle21.IsCommercialOracle, Is.True);

        var ms21 = new JavaVersion("21.0.0", "", "Microsoft", "x86_64");
        Assert.That(ms21.IsCommercialOracle, Is.False);
    }
}
