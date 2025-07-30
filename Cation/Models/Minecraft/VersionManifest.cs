using System;
using System.Collections.Generic;

namespace Cation.Models.Minecraft;

public class VersionManifest
{
    public required LatestInfo Latest { get; set; }

    public required List<VersionInfo> Versions { get; set; }

    public class LatestInfo
    {
        public required string Release { get; set; }

        public required string Snapshot { get; set; }

        public override string ToString()
        {
            return $"{Release}";
        }
    }

    public class VersionInfo
    {
        public required string Id { get; set; }

        public required string Type { get; set; }

        public required string Url { get; set; }

        public required DateTime Time { get; set; }

        public required DateTime ReleaseTime { get; set; }

        public required string Sha1 { get; set; }

        public required int ComplianceLevel { get; set; }

        public override string ToString()
        {
            return $"{Id}, {Type}";
        }
    }
}
