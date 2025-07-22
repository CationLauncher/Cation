using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Cation.Core.Java;

public class MinecraftJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        var runtimes = new List<string>();
        if (OperatingSystem.IsWindows())
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            runtimes.Add(Path.Combine(local, "Packages", "Microsoft.4297127D64EC6_8wekyb3d8bbwe", "LocalCache",
                "Local", "runtime"));
        }
        else if (OperatingSystem.IsMacOS())
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            runtimes.Add(Path.Combine(local, "minecraft", "runtime"));
        }
        else
        {
            var home = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            runtimes.Add(Path.Combine(home, ".minecraft", "runtime"));
        }

        var result = new List<string>();
        foreach (var javaPaths in from runtime in runtimes
                 where Directory.Exists(runtime)
                 select JavaManager.FindJavaBinPathRecurse(runtime))
        {
            result.AddRange(javaPaths);
        }

        return result;
    }
}
