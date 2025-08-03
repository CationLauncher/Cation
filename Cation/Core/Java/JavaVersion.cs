using System;
using System.ComponentModel;

namespace Cation.Core.Java;

public class JavaVersion
{
    public Version Version { get; init; }
    public string VersionRaw { get; init; }
    public string Path { get; init; }
    public JavaVendor Vendor { get; init; }
    public string VendorRaw { get; init; }
    public JavaArch Arch { get; init; }
    public string ArchRaw { get; init; }

    public bool IsCommercialOracle => Vendor == JavaVendor.Oracle && Version >= new Version(8, 0, 211);

    public JavaVersion(string version, string path, string vendor, string arch)
    {
        VersionRaw = version;
        Path = path;
        VendorRaw = vendor;
        ArchRaw = arch;

        var versionUnified = version;
        if (versionUnified.StartsWith("1."))
        {
            versionUnified = versionUnified[2..];
            versionUnified = versionUnified.Replace("_", ".");
        }

        Version = Version.Parse(versionUnified);

        Vendor = vendor switch
        {
            "Oracle Corporation" => JavaVendor.Oracle,
            "Microsoft" => JavaVendor.Microsoft,
            _ => JavaVendor.Unknown
        };

        Arch = arch switch
        {
            "x86" => JavaArch.X86,
            "i386" => JavaArch.X86,
            "i486" => JavaArch.X86,
            "i586" => JavaArch.X86,
            "i686" => JavaArch.X86,
            "x86_64" => JavaArch.X64,
            "amd64" => JavaArch.X64,
            "arm" => JavaArch.Arm32,
            "armhf" => JavaArch.Arm32,
            "aarch64" => JavaArch.Arm64,
            _ => JavaArch.Unknown
        };
    }

    public override string ToString()
    {
        var arch = Arch == JavaArch.Unknown ? ArchRaw : GetJavaArchDescription(Arch);
        return $"{VersionRaw} ({VendorRaw}) {arch}";
    }

    public static string GetJavaArchDescription(JavaArch arch)
    {
        var type = typeof(JavaArch);
        var member = type.GetMember(arch.ToString());
        if (member.Length <= 0)
            return arch.ToString();
        var attrs = member[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
        return attrs.Length > 0 ? ((DescriptionAttribute)attrs[0]).Description : arch.ToString();
    }

    public enum JavaVendor
    {
        [Description("Unknown")]
        Unknown,

        [Description("Oracle")]
        Oracle,

        [Description("Microsoft")]
        Microsoft,
    }

    public enum JavaArch
    {
        [Description("Unknown")]
        Unknown,

        [Description("x86")]
        X86,

        [Description("x86_64")]
        X64,

        [Description("ARM32")]
        Arm32,

        [Description("ARM64")]
        Arm64,
    }
}
