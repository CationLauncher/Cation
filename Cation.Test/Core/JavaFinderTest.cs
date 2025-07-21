using Cation.Core.Java;

namespace Cation.Test.Core;

public class JavaFinderTest
{
    [Test]
    public void Find()
    {
        Assert.That(JavaFinder.Find(), Is.Not.Empty);
    }
}
