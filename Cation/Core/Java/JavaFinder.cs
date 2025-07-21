using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cation.Core.Java;

public static class JavaFinder
{
    private static readonly List<IJavaFinder> Finders =
    [
        new EnvJavaFinder(),
        new MinecraftJavaFinder()
    ];

    static JavaFinder()
    {
        if (OperatingSystem.IsMacOS())
            Finders.Add(new MacJavaFinder());
    }

    public static IEnumerable<string> Find()
    {
        var result = new HashSet<string>();
        foreach (var path in Finders.SelectMany(finder => finder.Find()))
            result.Add(Path.GetFullPath(path));
        return result;
    }

    private static string JavaExecutableExtension => OperatingSystem.IsWindows() ? ".exe" : "";
    public static string JavaExecutableName => "java" + JavaExecutableExtension;

    public static List<string> FindJavaBinPathRecurse(string root)
    {
        var result = new List<string>();

        Recurse(root);
        return result;

        void Recurse(string path)
        {
            try
            {
                var bin = Path.Combine(path, "bin");
                var javaPath = Path.Combine(bin, JavaExecutableName);
                if (File.Exists(javaPath))
                {
                    result.Add(bin);
                    return;
                }

                foreach (var dir in Directory.EnumerateDirectories(path))
                {
                    Recurse(dir);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}
