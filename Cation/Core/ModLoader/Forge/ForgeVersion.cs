using System;

namespace Cation.Core.ModLoader.Forge;

public class ForgeVersion(
    Version version,
    string versionString,
    Version minecraftVersion,
    string minecraftVersionString)
{
    public Version Version { get; init; } = version;
    public string VersionString { get; init; } = versionString;
    public Version MinecraftVersion { get; init; } = minecraftVersion;
    public string MinecraftVersionString { get; init; } = minecraftVersionString;

    public override string ToString()
    {
        return $"{MinecraftVersionString} - {VersionString}";
    }
}
