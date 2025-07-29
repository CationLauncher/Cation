using System.Collections.Generic;

namespace Cation.Core.Java.JavaFinder;

public interface IJavaFinder
{
    IEnumerable<string> Find();
}
