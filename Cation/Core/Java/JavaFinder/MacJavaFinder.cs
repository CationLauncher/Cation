using System.Collections.Generic;

namespace Cation.Core.Java.JavaFinder;

public class MacJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        return Utils.FindJavaBinPathRecurse("/Library/Java/JavaVirtualMachines");
    }
}
