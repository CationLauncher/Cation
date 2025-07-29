using Cation.Core.Java.JavaFinder;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Cation.Core.Java;

public static class JavaManager
{
    private static readonly List<IJavaFinder> Finders =
    [
        new EnvJavaFinder(),
        new MinecraftJavaFinder()
    ];

    static JavaManager()
    {
        if (OperatingSystem.IsMacOS())
            Finders.Add(new MacJavaFinder());
    }

    public static IEnumerable<string> FindJavaPath()
    {
        var result = new HashSet<string>();
        foreach (var path in Finders.SelectMany(finder => finder.Find()))
        {
            try
            {
                var linkTarget = new FileInfo(path).ResolveLinkTarget(true);
                var finalPath = linkTarget == null ? path : linkTarget.FullName;
                result.Add(Path.GetFullPath(finalPath));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        return result;
    }

    private static string JavaExecutableExtension => OperatingSystem.IsWindows() ? ".exe" : "";
    public static string JavaExecutableName => "java" + JavaExecutableExtension;

    public static List<JavaVersion> GetJavaList()
    {
        var paths = FindJavaPath();
        var result = new List<JavaVersion>();
        foreach (var path in paths)
        {
            var java = Path.Combine(path, JavaExecutableName);
            var classPath = AppContext.BaseDirectory;
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = java,
                    Arguments = "JavaTest",
                    WorkingDirectory = classPath,
                    RedirectStandardOutput = true,
                    CreateNoWindow = true,
                }
            };
            try
            {
                process.Start();
                if (!process.WaitForExit(5000))
                {
                    process.Kill();
                    throw new TimeoutException("Java process timeout");
                }

                if (process.ExitCode != 0)
                    throw new Exception("Java process exited with code " + process.ExitCode);

                var output = process.StandardOutput.ReadToEnd();
                var lines = output.Split('\n', StringSplitOptions.RemoveEmptyEntries);

                string rawArch = "", rawVersion = "", rawVendor = "";

                foreach (var line in lines)
                {
                    var trimmed = line.Trim();
                    var parts = trimmed.Split('=', 2);
                    if (parts.Length != 2)
                        continue;

                    var key = parts[0].Trim();
                    var value = parts[1].Trim();

                    switch (key)
                    {
                        case "os.arch":
                            rawArch = value;
                            break;
                        case "java.version":
                            rawVersion = value;
                            break;
                        case "java.vendor":
                            rawVendor = value;
                            break;
                    }
                }

                if (rawArch == "" || rawVersion == "" || rawVendor == "")
                    throw new Exception("Can not get Java info");

                result.Add(new JavaVersion(rawVersion, path, rawVendor, rawArch));
            }
            catch
            {
                Console.WriteLine($"Java test failed: {java}");
            }
        }

        return result.OrderByDescending(jv => jv.Version).ToList();
    }
}
