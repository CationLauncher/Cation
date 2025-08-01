using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using System;
using System.Linq;

namespace Cation.Core.ModLoader.Forge;

public static class ForgeVersionList
{
    private const string ForgeMavenMetadataUrl =
        "https://maven.minecraftforge.net/net/minecraftforge/forge/maven-metadata.xml";

    public static async Task<List<ForgeVersion>> GetForgeVersionListAsync()
    {
        var httpClient = new HttpClient();
        await using var stream = await httpClient.GetStreamAsync(ForgeMavenMetadataUrl);
        var doc = XDocument.Load(stream);

        var groupId = doc.Root?.Element("groupId")?.Value;
        var artifactId = doc.Root?.Element("artifactId")?.Value;
        if (groupId != "net.minecraftforge" || artifactId != "forge")
            return [];

        var versions = doc.Root?.Element("versioning")?.Element("versions")?.Elements("version");
        if (versions == null)
            return [];

        var result = new List<ForgeVersion>();
        foreach (var version in versions)
        {
            var splits = version.Value.Split('-');
            if (splits.Length != 2)
                continue;

            var minecraftVersionStr = splits[0];
            var forgeVersionStr = splits[1];
            if (VersionFromString(forgeVersionStr, out var forgeVersion) && forgeVersion != null &&
                VersionFromString(minecraftVersionStr, out var minecraftVersion) && minecraftVersion != null)
            {
                result.Add(new ForgeVersion(forgeVersion, forgeVersionStr, minecraftVersion, minecraftVersionStr));
            }
        }

        return result.OrderByDescending(v => v.Version).ToList();
    }

    private static bool VersionFromString(string versionString, out Version? version)
    {
        return Version.TryParse(versionString.Split('-')[0].Split('_')[0], out version);
    }
}
