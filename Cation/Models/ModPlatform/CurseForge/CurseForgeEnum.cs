namespace Cation.Models.ModPlatform.CurseForge;

public enum ClassId
{
    BukkitPlugin = 5,
    Mod = 6,
    ResourcePack = 12,
    World = 17,
    ModPack = 4471,
    Customization = 4546,
    Addon = 4559,
    Shader = 6552,
    DataPack = 6945,
}

public enum FileReleaseType
{
    Release = 1,
    Beta,
    Alpha
}

public enum ModsSearchSortFieldLegacy
{
    Relevancy = 1,
    Popularity,
    Latest,
    Trending,
    Created,
    DownloadCount,
    NameAsc,
    NameDesc
}

public enum ModStatus
{
    New = 1,
    ChangesRequired,
    UnderSoftReview,
    Approved,
    Rejected,
    ChangesMade,
    Inactive,
    Abandoned,
    Deleted,
    UnderReview
}
