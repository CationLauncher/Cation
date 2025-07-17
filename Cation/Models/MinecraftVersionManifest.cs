using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cation.Models;

public static class MinecraftVersionManifest
{
    public class LatestInfo
    {
        [JsonPropertyName("release")]
        public string Release { get; set; }
        [JsonPropertyName("snapshot")]
        public string Snapshot { get; set; }
    }

    public class VersionEntry
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("url")]
        public string Url { get; set; }
        [JsonPropertyName("time")]
        public string Time { get; set; }
        [JsonPropertyName("releaseTime")]
        public string ReleaseTime { get; set; }
        [JsonPropertyName("sha1")]
        public string Sha1 { get; set; }
        [JsonPropertyName("complianceLevel")]
        public int ComplianceLevel { get; set; }
    }

    public class VersionManifest
    {
        [JsonPropertyName("latest")]
        public LatestInfo Latest { get; set; }
        [JsonPropertyName("versions")]
        public List<VersionEntry> Versions { get; set; }
    }
}
