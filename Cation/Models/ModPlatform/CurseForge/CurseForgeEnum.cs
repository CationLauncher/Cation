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

public enum FileStatus
{
    Processing = 1,
    ChangesRequired,
    UnderReview,
    Approved,
    Rejected,
    MalwareDetected,
    Deleted,
    Archived,
    Testing,
    Released,
    ReadyForReview,
    Deprecated,
    Baking,
    AwaitingPublishing,
    FailedPublishing,
    Cooking,
    Cooked,
    UnderManualReview,
    ScanningForMalware,
    ProcessingFile,
    PendingRelease,
    ReadyForCooking,
    PostProcessing
}

public enum FileRelationType
{
    EmbeddedLibrary = 1,
    OptionalDependency,
    RequiredDependency,
    Tool,
    Incompatible,
    Include
}

public enum HashAlgo
{
    Sha1 = 1,
    Md5
}

public enum ModsSearchSortField
{
    Featured = 1,
    Popularity,
    LastUpdated,
    Name,
    Author,
    TotalDownloads,
    Category,
    GameVersion,
    EarlyAccess,
    FeaturedReleased,
    ReleasedDate,
    Rating
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

public enum ModLoaderType
{
    Any,
    Forge,
    Cauldron,
    LiteLoader,
    Fabric,
    Quilt,
    NeoForge
}
