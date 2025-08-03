using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cation.Core.Java.JavaFinder;

public class MacJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        if (!OperatingSystem.IsMacOS())
            return [];

        var searchDirs = new[]
        {
            "/Library/Java/JavaVirtualMachines",
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Library/Java/JavaVirtualMachines")
        };
        return (from baseDir in searchDirs
            where Directory.Exists(baseDir)
            from dir in Directory.GetDirectories(baseDir)
            select Path.Combine(dir, "Contents", "Home", "bin")
            into binPath
            let javaPath = Path.Combine(binPath, JavaManager.JavaExecutableName)
            where File.Exists(javaPath)
            select binPath).ToList();
    }
}
