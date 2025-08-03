using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace Cation.Core.Java.JavaFinder;

public class RegistryJavaFinder : IJavaFinder
{
    public IEnumerable<string> Find()
    {
        if (!OperatingSystem.IsWindows())
            return [];

        var result = new List<string>();
        string[] registryKeys =
        [
            @"SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Uninstall",
            @"SOFTWARE\\WOW6432Node\\Microsoft\\Windows\\CurrentVersion\\Uninstall"
        ];
        foreach (var root in new[] { Registry.LocalMachine, Registry.CurrentUser })
        {
            foreach (var keyPath in registryKeys)
            {
                using var key = root.OpenSubKey(keyPath);
                if (key == null)
                    continue;
                foreach (var subKeyName in key.GetSubKeyNames())
                {
                    using var subKey = key.OpenSubKey(subKeyName);
                    var displayName = subKey?.GetValue("DisplayName") as string;
                    var installLocation = subKey?.GetValue("InstallLocation") as string;
                    if (string.IsNullOrEmpty(displayName) || string.IsNullOrEmpty(installLocation))
                        continue;
                    if (displayName.Contains(" JDK ") || displayName.Contains(" OpenJDK ") ||
                        displayName.Contains(" JRE ") || displayName.StartsWith("Java(TM) SE Development Kit") ||
                        displayName.StartsWith("Java 8 Update "))
                    {
                        result.AddRange(Utils.FindJavaBinPathRecurse(installLocation));
                    }
                }
            }
        }

        return result;
    }
}
