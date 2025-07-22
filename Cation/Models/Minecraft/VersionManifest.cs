using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cation.Models.Minecraft;

public class VersionManifest
{
    [JsonPropertyName("latest")]
    public required LatestInfo Latest { get; set; }

    [JsonPropertyName("versions")]
    public required List<VersionInfo> Versions { get; set; }

    public class LatestInfo
    {
        [JsonPropertyName("release")]
        public required string Release { get; set; }

        [JsonPropertyName("snapshot")]
        public required string Snapshot { get; set; }

        public override string ToString()
        {
            return $"{Release}";
        }
    }

    public class VersionInfo
    {
        [JsonPropertyName("id")]
        public required string Id { get; set; }

        [JsonPropertyName("type")]
        public required string Type { get; set; }

        [JsonPropertyName("url")]
        public required string Url { get; set; }

        [JsonPropertyName("time")]
        public required DateTime Time { get; set; }

        [JsonPropertyName("releaseTime")]
        public required DateTime ReleaseTime { get; set; }

        [JsonPropertyName("sha1")]
        public required string Sha1 { get; set; }

        [JsonPropertyName("complianceLevel")]
        public required int ComplianceLevel { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Type}";
        }
    }
}
