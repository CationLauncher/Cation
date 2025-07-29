using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cation.Core.Java.JavaFinder;

public class EnvJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        var result = new List<string>();

        // JAVA_HOME
        var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
        if (!string.IsNullOrEmpty(javaHome))
        {
            var binPath = Path.Combine(javaHome, "bin");
            var javaPath = Path.Combine(binPath, JavaManager.JavaExecutableName);
            if (File.Exists(javaPath))
                result.Add(binPath);
        }

        // PATH
        var path = Environment.GetEnvironmentVariable("PATH");
        if (!string.IsNullOrEmpty(path))
        {
            result.AddRange(from p in path.Split(Path.PathSeparator)
                let javaPath = Path.Combine(p, JavaManager.JavaExecutableName)
                where File.Exists(javaPath)
                where !(OperatingSystem.IsMacOS() && javaPath == "/usr/bin/java") // Ignore MacOS java loader
                select p);
        }

        return result;
    }
}
