using Cation.Core.Java;

namespace Cation.Test.Core;

public class JavaManagerTest
{
    [Test]
    public void Find()
    {
        var result = JavaManager.Find();
        Assert.That(result, Is.Not.Empty);
    }

    [Test]
    public void GetJavaList()
    {
        var result = JavaManager.GetJavaList();
        Assert.That(result, Is.Not.Empty);
    }
}
