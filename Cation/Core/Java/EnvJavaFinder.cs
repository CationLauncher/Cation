using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cation.Core.Java;

public class EnvJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        var result = new List<string>();

        // JAVA_HOME
        var javaHome = Environment.GetEnvironmentVariable("JAVA_HOME");
        if (!string.IsNullOrEmpty(javaHome))
        {
            var javaPath = Path.Combine(javaHome, "bin", JavaFinder.JavaExecutableName);
            if (File.Exists(javaPath))
                result.Add(javaPath);
        }

        // PATH
        var path = Environment.GetEnvironmentVariable("PATH");
        if (!string.IsNullOrEmpty(path))
        {
            result.AddRange(from p in path.Split(Path.PathSeparator)
                select Path.Combine(p, JavaFinder.JavaExecutableName)
                into javaPath
                where File.Exists(javaPath)
                where !(javaPath == "/usr/bin/java" && OperatingSystem.IsMacOS()) // Ignore MacOS java loader
                select javaPath);
        }

        return result;
    }
}
