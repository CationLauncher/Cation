using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cation.Models.Minecraft;

public class Client
{
    [JsonPropertyName("arguments")]
    public ArgumentsInfo? Arguments { get; set; }

    [JsonPropertyName("assetIndex")]
    public AssetIndexInfo? AssetIndex { get; set; }

    [JsonPropertyName("assets")]
    public string Assets { get; set; } = string.Empty;

    [JsonPropertyName("complianceLevel")]
    public int ComplianceLevel { get; set; }

    [JsonPropertyName("downloads")]
    public DownloadsInfo? Downloads { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    [JsonPropertyName("javaVersion")]
    public JavaVersionInfo? JavaVersion { get; set; }

    [JsonPropertyName("libraries")]
    public List<LibraryInfo>? Libraries { get; set; }

    [JsonPropertyName("logging")]
    public LoggingInfo? Logging { get; set; }

    [JsonPropertyName("mainClass")]
    public string MainClass { get; set; } = string.Empty;

    [JsonPropertyName("minecraftArguments")]
    public string? MinecraftArguments { get; set; }

    [JsonPropertyName("minimumLauncherVersion")]
    public int MinimumLauncherVersion { get; set; }

    [JsonPropertyName("releaseTime")]
    public DateTime ReleaseTime { get; set; }

    [JsonPropertyName("time")]
    public DateTime Time { get; set; }

    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    public class AssetIndexInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("totalSize")]
        public long TotalSize { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class DownloadInfo
    {
        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class DownloadsInfo
    {
        [JsonPropertyName("client")]
        public DownloadInfo? Client { get; set; }

        [JsonPropertyName("client_mappings")]
        public DownloadInfo? ClientMappings { get; set; }

        [JsonPropertyName("server")]
        public DownloadInfo? Server { get; set; }

        [JsonPropertyName("server_mappings")]
        public DownloadInfo? ServerMappings { get; set; }
    }

    public class JavaVersionInfo
    {
        [JsonPropertyName("component")]
        public string Component { get; set; } = string.Empty;

        [JsonPropertyName("majorVersion")]
        public int MajorVersion { get; set; }
    }

    public class ArtifactInfo
    {
        [JsonPropertyName("path")]
        public string Path { get; set; } = string.Empty;

        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class LibraryDownloadsInfo
    {
        [JsonPropertyName("artifact")]
        public ArtifactInfo? Artifact { get; set; }

        [JsonPropertyName("classifiers")]
        public Dictionary<string, ArtifactInfo>? Classifiers { get; set; }
    }

    public class OsInfo
    {
        [JsonPropertyName("name")]
        public string? Name { get; set; }

        [JsonPropertyName("version")]
        public string? Version { get; set; }

        [JsonPropertyName("arch")]
        public string? Arch { get; set; }
    }

    public class RuleInfo
    {
        [JsonPropertyName("action")]
        public string Action { get; set; } = string.Empty;

        [JsonPropertyName("os")]
        public OsInfo? Os { get; set; }

        [JsonPropertyName("features")]
        public Dictionary<string, bool>? Features { get; set; }
    }

    public class ExtractInfo
    {
        [JsonPropertyName("exclude")]
        public List<string>? Exclude { get; set; }
    }

    public class ArgumentsInfo
    {
        [JsonPropertyName("game")]
        public List<object>? Game { get; set; }

        [JsonPropertyName("jvm")]
        public List<object>? Jvm { get; set; }
    }

    public class LibraryInfo
    {
        [JsonPropertyName("downloads")]
        public LibraryDownloadsInfo? Downloads { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("rules")]
        public List<RuleInfo>? Rules { get; set; }

        [JsonPropertyName("natives")]
        public Dictionary<string, string>? Natives { get; set; }

        [JsonPropertyName("extract")]
        public ExtractInfo? Extract { get; set; }
    }

    public class LoggingFileInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; } = string.Empty;

        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; } = string.Empty;

        [JsonPropertyName("size")]
        public long Size { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; } = string.Empty;
    }

    public class LoggingClientInfo
    {
        [JsonPropertyName("argument")]
        public string Argument { get; set; } = string.Empty;

        [JsonPropertyName("file")]
        public LoggingFileInfo? File { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
    }

    public class LoggingInfo
    {
        [JsonPropertyName("client")]
        public LoggingClientInfo? Client { get; set; }
    }
}
