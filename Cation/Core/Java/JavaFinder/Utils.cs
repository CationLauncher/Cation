using System;
using System.Collections.Generic;
using System.IO;

namespace Cation.Core.Java.JavaFinder;

public class Utils
{
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
                var javaPath = Path.Combine(bin, JavaManager.JavaExecutableName);
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
