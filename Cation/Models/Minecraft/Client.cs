using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Cation.Models.Minecraft;

public class Client
{
    public ArgumentsInfo? Arguments { get; set; }
    public AssetIndexInfo? AssetIndex { get; set; }
    public string Assets { get; set; } = string.Empty;
    public int ComplianceLevel { get; set; }
    public DownloadsInfo? Downloads { get; set; }
    public string Id { get; set; } = string.Empty;
    public JavaVersionInfo? JavaVersion { get; set; }
    public List<LibraryInfo>? Libraries { get; set; }
    public LoggingInfo? Logging { get; set; }
    public string MainClass { get; set; } = string.Empty;
    public string? MinecraftArguments { get; set; }
    public int MinimumLauncherVersion { get; set; }
    public DateTime ReleaseTime { get; set; }
    public DateTime Time { get; set; }
    public string Type { get; set; } = string.Empty;

    public class AssetIndexInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Sha1 { get; set; } = string.Empty;
        public long Size { get; set; }
        public long TotalSize { get; set; }
        public string Url { get; set; } = string.Empty;
    }

    public class DownloadInfo
    {
        public string Sha1 { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Url { get; set; } = string.Empty;
    }

    public class DownloadsInfo
    {
        public DownloadInfo? Client { get; set; }
        public DownloadInfo? ClientMappings { get; set; }
        public DownloadInfo? Server { get; set; }
        public DownloadInfo? ServerMappings { get; set; }
    }

    public class JavaVersionInfo
    {
        public string Component { get; set; } = string.Empty;
        public int MajorVersion { get; set; }
    }

    public class ArtifactInfo
    {
        public string Path { get; set; } = string.Empty;
        public string Sha1 { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Url { get; set; } = string.Empty;
    }

    public class LibraryDownloadsInfo
    {
        public ArtifactInfo? Artifact { get; set; }
        public Dictionary<string, ArtifactInfo>? Classifiers { get; set; }
    }

    public class OsInfo
    {
        public string? Name { get; set; }
        public string? Version { get; set; }
        public string? Arch { get; set; }
    }

    public class RuleInfo
    {
        public string Action { get; set; } = string.Empty;
        public OsInfo? Os { get; set; }
        public Dictionary<string, bool>? Features { get; set; }
    }

    public class ExtractInfo
    {
        public List<string>? Exclude { get; set; }
    }

    public class ArgumentsInfo
    {
        public List<JsonElement>? Game { get; set; }
        public List<JsonElement>? Jvm { get; set; }
    }

    public class LibraryInfo
    {
        public LibraryDownloadsInfo? Downloads { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<RuleInfo>? Rules { get; set; }
        public Dictionary<string, string>? Natives { get; set; }
        public ExtractInfo? Extract { get; set; }
    }

    public class LoggingFileInfo
    {
        public string Id { get; set; } = string.Empty;
        public string Sha1 { get; set; } = string.Empty;
        public long Size { get; set; }
        public string Url { get; set; } = string.Empty;
    }

    public class LoggingClientInfo
    {
        public string Argument { get; set; } = string.Empty;
        public LoggingFileInfo? File { get; set; }
        public string Type { get; set; } = string.Empty;
    }

    public class LoggingInfo
    {
        public LoggingClientInfo? Client { get; set; }
    }
}
