using System.Collections.Generic;

namespace Cation.Core.Java;

public class MacJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        return JavaManager.FindJavaBinPathRecurse("/Library/Java/JavaVirtualMachines");
    }
}
