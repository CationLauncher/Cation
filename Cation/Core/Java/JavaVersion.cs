using System;

namespace Cation.Core.Java;

public class JavaVersion(Version version, string path, string vendor, string arch)
{
    public Version Version { get; init; } = version;
    public string Path { get; init; } = path;
    public string Vendor { get; init; } = vendor;
    public string Arch { get; init; } = arch;

    public override string ToString()
    {
        return $"{Version} ({Vendor}) {Arch}";
    }
}
