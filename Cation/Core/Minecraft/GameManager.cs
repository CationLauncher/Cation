using Cation.Models.Minecraft;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace Cation.Core.Minecraft;

public static class GameManager
{
    private static readonly string MinecraftOsName;
    private static readonly string MinecraftOsVersion;
    private static readonly string MinecraftOsArch;

    static GameManager()
    {
        if (OperatingSystem.IsMacOS())
            MinecraftOsName = "osx";
        else if (OperatingSystem.IsWindows())
            MinecraftOsName = "windows";
        else
            MinecraftOsName = "linux";
        MinecraftOsVersion = Environment.OSVersion.Version.ToString();
        MinecraftOsArch = Environment.Is64BitOperatingSystem ? "x64" : "x86";
    }

    private static string GetDefaultMinecraftPath()
    {
        if (OperatingSystem.IsMacOS())
        {
            var local = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(local, "minecraft");
        }

        if (OperatingSystem.IsWindows())
        {
            var roaming = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            return Path.Combine(roaming, ".minecraft");
        }

        var profile = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        return Path.Combine(profile, ".minecraft");
    }

    public static List<string> GetGameInstances(string? minecraftPath = null)
    {
        minecraftPath = minecraftPath switch
        {
            null => GetDefaultMinecraftPath(),
            "" => Path.Combine(Directory.GetCurrentDirectory(), ".minecraft"),
            _ => minecraftPath
        };

        var result = new List<string>();
        var versionsPath = Path.Combine(minecraftPath, "versions");
        if (!Directory.Exists(versionsPath))
            return result;
        result.AddRange(from instancePath in Directory.GetDirectories(versionsPath)
            let instanceName = Path.GetFileName(instancePath)
            let jsonPath = Path.Combine(instancePath, instanceName + ".json")
            let jarPath = Path.Combine(instancePath, instanceName + ".jar")
            where File.Exists(jsonPath) && File.Exists(jarPath)
            select instancePath);
        return result;
    }

    public static string? GetGameArguments(string gameInstance, string username, string userType, string userId = "",
        string accessToken = "")
    {
        var instanceName = Path.GetFileName(gameInstance);
        var jsonPath = Path.Combine(gameInstance, instanceName + ".json");
        using var stream = File.OpenRead(jsonPath);
        var versionManifest = MinecraftJsonContext.Deserialize<Client>(stream);
        if (versionManifest == null)
        {
            Console.WriteLine($"Failed to deserialize {jsonPath}");
            return null;
        }

        var minecraftPath = Path.GetDirectoryName(Path.GetDirectoryName(gameInstance));
        if (minecraftPath == null)
        {
            Console.WriteLine($"Failed to get Minecraft path from {gameInstance}");
            return null;
        }

        Dictionary<string, string> variables = new()
        {
            { "auth_player_name", username },
            { "version_name", versionManifest.Id },
            { "game_directory", $"\"{minecraftPath}\"" },
            { "game_assets", $"\"{Path.Combine(minecraftPath, "assets", "virtual", versionManifest.Assets)}\"" },
            { "assets_root", $"\"{Path.Combine(minecraftPath, "assets")}\"" },
            { "assets_index_name", versionManifest.AssetIndex?.Id ?? "legacy" },
            { "auth_session", "\"\"" },
            { "auth_uuid", $"\"{userId}\"" },
            { "auth_access_token", $"\"{accessToken}\"" },
            { "clientid", "\"\"" },
            { "auth_xuid", "\"\"" },
            { "user_type", userType },
            { "version_type", versionManifest.Type },
            { "classpath", $"\"{GenerateClassPath(minecraftPath, versionManifest)}\"" },
            { "main_class", versionManifest.MainClass },
            { "launcher_name", "Cation" },
            { "launcher_version", "1.0" },
            { "natives_directory", $"\"{Path.Combine(minecraftPath, "versions", versionManifest.Id, "natives")}\"" }
        };

        List<string> argsTemplate = [];

        if (versionManifest.Arguments is { Jvm: not null, Game: not null })
        {
            // New arguments from 1.13
            foreach (var jvmArg in versionManifest.Arguments.Jvm)
            {
                if (jvmArg.ValueKind == JsonValueKind.String)
                    argsTemplate.Add(jvmArg.GetString() ?? "");
                else if (jvmArg.ValueKind == JsonValueKind.Object)
                {
                    if (!jvmArg.TryGetProperty("rules", out var rulesElement) ||
                        !jvmArg.TryGetProperty("value", out var valueElement))
                        continue;
                    var rules = MinecraftJsonContext.Deserialize<List<Client.RuleInfo>>(rulesElement);
                    if (rules == null)
                        continue;
                    if (!MatchRules(rules))
                        continue;
                    if (valueElement.ValueKind == JsonValueKind.String)
                    {
                        var value = valueElement.GetString() ?? "";
                        argsTemplate.Add(value);
                    }
                    else if (valueElement.ValueKind == JsonValueKind.Array)
                    {
                        var values = MinecraftJsonContext.Deserialize<List<string>>(valueElement);
                        if (values == null)
                            continue;
                        argsTemplate.AddRange(values);
                    }
                }
            }

            argsTemplate.AddRange([
                "-Xmx2G",
                "${main_class}"
            ]);

            argsTemplate.AddRange(versionManifest.Arguments.Game.Where(arg => arg.ValueKind == JsonValueKind.String)
                .Select(arg => arg.GetString() ?? ""));
        }
        else if (versionManifest.MinecraftArguments != null)
        {
            // Old arguments
            argsTemplate =
            [
                "-Djava.library.path=${natives_directory}",
                "-Dminecraft.launcher.brand=${launcher_name}",
                "-Dminecraft.launcher.version=${launcher_version}",
                "-cp ${classpath}",
                "-Xmx2G",
                "${main_class}"
            ];
            argsTemplate.Add(versionManifest.MinecraftArguments);
        }
        else
        {
            // Should this happen?
            return null;
        }

        List<string> args = [];
        args.AddRange(argsTemplate.Select(template => ReplaceVariable(template, variables)));
        return string.Join(" ", args);
    }

    private static string ReplaceVariable(string template, Dictionary<string, string> variables)
    {
        if (string.IsNullOrEmpty(template) || variables.Count == 0)
            return template;
        return variables.Aggregate(template, (current, kv) => current.Replace($"${{{kv.Key}}}", kv.Value));
    }

    private static string GenerateClassPath(string minecraftPath, Client versionManifest)
    {
        if (versionManifest.Libraries == null || versionManifest.Libraries.Count == 0)
            return string.Empty;

        var instancePath = Path.Combine(minecraftPath, "versions", versionManifest.Id);
        var nativesPath = Path.Combine(instancePath, "natives");
        if (!Directory.Exists(nativesPath))
            Directory.CreateDirectory(nativesPath);

        List<string> libraries = [];
        foreach (var library in versionManifest.Libraries)
        {
            if (!MatchRules(library.Rules))
                continue;

            var path = library.Downloads?.Artifact?.Path;
            if (path != null)
            {
                libraries.Add(Path.Join(minecraftPath, "libraries", path));
            }
            else if (library.Downloads?.Classifiers != null && library.Natives != null)
            {
                library.Natives.TryGetValue(MinecraftOsName, out var classifier);
                if (classifier == null)
                    continue;
                if (!library.Downloads.Classifiers.TryGetValue(classifier, out var nativeArtifact))
                    continue;
                var nativeLibPath = Path.Combine(minecraftPath, "libraries", nativeArtifact.Path);
                if (!File.Exists(nativeLibPath))
                    continue;
                using var archive = ZipFile.OpenRead(nativeLibPath);
                foreach (var entry in archive.Entries)
                {
                    if (entry.FullName.StartsWith("META-INF/"))
                        continue;
                    var destinationPath = Path.Combine(nativesPath, entry.FullName);
                    var destinationDir = Path.GetDirectoryName(destinationPath);
                    if (!string.IsNullOrEmpty(destinationDir) && !Directory.Exists(destinationDir))
                        Directory.CreateDirectory(destinationDir);
                    if (!File.Exists(destinationPath))
                        entry.ExtractToFile(destinationPath);
                }
            }
        }

        // Game jar
        libraries.Add(Path.Join(instancePath, versionManifest.Id + ".jar"));

        return string.Join(OperatingSystem.IsWindows() ? ";" : ":", libraries);
    }

    private static bool MatchRules(List<Client.RuleInfo>? rules)
    {
        if (rules == null || rules.Count == 0)
            return true;

        bool allow = false;

        foreach (var rule in rules)
        {
            var action = rule.Action == "allow";
            if (rule.Os == null)
            {
                allow = action;
                continue;
            }

            bool nameMatch = rule.Os.Name == null || rule.Os.Name == MinecraftOsName;
            bool versionMatch = rule.Os.Version == null || Regex.IsMatch(MinecraftOsVersion, rule.Os.Version);
            bool archMatch = rule.Os.Arch == null || rule.Os.Arch == MinecraftOsArch;

            if (nameMatch && versionMatch && archMatch)
                allow = action;
        }

        return allow;
    }
}
