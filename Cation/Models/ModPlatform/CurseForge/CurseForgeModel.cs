using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cation.Models.ModPlatform.CurseForge;

public class Category
{
    /// <summary>
    /// The mod id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The game id related to the category
    /// </summary>
    public required int GameId { get; set; }

    /// <summary>
    /// Category name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The category slug as it appear in the URL
    /// </summary>
    public required string Slug { get; set; }

    /// <summary>
    /// The category URL
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// URL for the category icon
    /// </summary>
    public required string IconUrl { get; set; }

    /// <summary>
    /// Last modified date of the category
    /// </summary>
    public required DateTime DateModified { get; set; }

    /// <summary>
    /// A top level category for other categories
    /// </summary>
    public bool? IsClass { get; set; }

    /// <summary>
    /// The class id of the category, meaning - the class of which this category is under
    /// </summary>
    public int? ClassId { get; set; }

    /// <summary>
    /// The parent category for this category
    /// </summary>
    public int? ParentCategoryId { get; set; }

    /// <summary>
    /// The display index for this category
    /// </summary>
    public int? DisplayIndex { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Id})";
    }
}

public class ModLinks
{
    /// <summary>
    /// Mod website URL
    /// </summary>
    public string? WebsiteUrl { get; set; }

    /// <summary>
    /// Mod Wiki URL
    /// </summary>
    public string? WikiUrl { get; set; }

    /// <summary>
    /// Mod issues URL
    /// </summary>
    public string? IssuesUrl { get; set; }

    /// <summary>
    /// Mod source URL
    /// </summary>
    public string? SourceUrl { get; set; }
}

public class FileIndex
{
    /// <summary>
    /// The compatible game version for this file
    /// </summary>
    public required string GameVersion { get; set; }

    /// <summary>
    /// The file id
    /// </summary>
    public required int FileId { get; set; }

    /// <summary>
    /// The file name
    /// </summary>
    [JsonPropertyName("filename")]
    public required string FileName { get; set; }

    /// <summary>
    /// The file release type
    /// </summary>
    public required FileReleaseType ReleaseType { get; set; }

    /// <summary>
    /// Game version type id
    /// </summary>
    public int? GameVersionTypeId { get; set; }

    /// <summary>
    /// Mod loader type
    /// </summary>
    public required ModLoaderType ModLoader { get; set; }

    public override string ToString()
    {
        return $"{FileName} ({FileId})";
    }
}

public class FileHash
{
    /// <summary>
    /// The hash value
    /// </summary>
    public required string Value { get; set; }

    /// <summary>
    /// The hash algorithm
    /// </summary>
    public required HashAlgo Algo { get; set; }
}

public class FileDependency
{
    /// <summary>
    /// The dependency mod id
    /// </summary>
    public required int ModId { get; set; }

    /// <summary>
    /// The relation type
    /// </summary>
    public required FileRelationType RelationType { get; set; }
}

public class FileModule
{
    /// <summary>
    /// The file module name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The file module fingerprint
    /// </summary>
    public required long Fingerprint { get; set; }
}

public class File
{
    /// <summary>
    /// The file id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The game id related to the mod that this file belongs to
    /// </summary>
    public required int GameId { get; set; }

    /// <summary>
    /// The mod id
    /// </summary>
    public required int ModId { get; set; }

    /// <summary>
    /// Whether the file is available to download
    /// </summary>
    public required bool IsAvailable { get; set; }

    /// <summary>
    /// Display name of the file
    /// </summary>
    public required string DisplayName { get; set; }

    /// <summary>
    /// Exact file name
    /// </summary>
    public required string FileName { get; set; }

    /// <summary>
    /// The file release type
    /// </summary>
    public required FileReleaseType ReleaseType { get; set; }

    /// <summary>
    /// Status of the file
    /// </summary>
    public required FileStatus FileStatus { get; set; }

    /// <summary>
    /// The file hash (i.e. md5 or sha1)
    /// </summary>
    public required List<FileHash> Hashes { get; set; }

    /// <summary>
    /// The file timestamp
    /// </summary>
    public required DateTime FileDate { get; set; }

    /// <summary>
    /// The file length in bytes
    /// </summary>
    public required long FileLength { get; set; }

    /// <summary>
    /// The number of downloads for the file
    /// </summary>
    public required long DownloadCount { get; set; }

    /// <summary>
    /// The file's size on disk
    /// </summary>
    public long? FileSizeOnDisk { get; set; }

    /// <summary>
    /// The file download URL
    /// </summary>
    public required string DownloadUrl { get; set; }

    /// <summary>
    /// List of game versions this file is relevant for
    /// </summary>
    public required List<string> GameVersions { get; set; }

    /// <summary>
    /// Metadata used for sorting by game versions
    /// </summary>
    public required List<SortableGameVersion> SortableGameVersions { get; set; }

    /// <summary>
    /// List of dependencies files
    /// </summary>
    public required List<FileDependency> Dependencies { get; set; }

    public bool? ExposeAsAlternative { get; set; }

    public int? ParentProjectFileId { get; set; }

    public int? AlternateFileId { get; set; }

    public bool? IsServerPack { get; set; }

    public int? ServerPackFileId { get; set; }

    public bool? IsEarlyAccessContent { get; set; }

    public DateTime? EarlyAccessEndDate { get; set; }

    public required long FileFingerprint { get; set; }

    public required List<FileModule> Modules { get; set; }

    public override string ToString()
    {
        return $"{FileName} ({Id})";
    }
}

public class SortableGameVersion
{
    /// <summary>
    /// Original version name (e.g. 1.5b)
    /// </summary>
    public required string GameVersionName { get; set; }

    /// <summary>
    /// Used for sorting (e.g. 0000000001.0000000005)
    /// </summary>
    public required string GameVersionPadded { get; set; }

    /// <summary>
    /// Game version clean name (e.g. 1.5)
    /// </summary>
    public required string GameVersion { get; set; }

    /// <summary>
    /// Game version release date
    /// </summary>
    public required DateTime GameVersionReleaseDate { get; set; }

    /// <summary>
    /// Game version type id
    /// </summary>
    public int? GameVersionTypeId { get; set; }

    public override string ToString()
    {
        return GameVersionName;
    }
}

public class GetCategoriesResponse
{
    /// <summary>
    /// The response data
    /// </summary>
    public required List<Category> Data { get; set; }
}

public class ModAuthor
{
    /// <summary>
    /// The mod author id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The mod author name
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The mod author URL
    /// </summary>
    public required string Url { get; set; }

    /// <summary>
    /// The mod author's avatar URL
    /// </summary>
    public required string AvatarUrl { get; set; }
}

public class ModAsset
{
    /// <summary>
    /// The mod asset id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The mod id this asset is for
    /// </summary>
    public required int ModId { get; set; }

    /// <summary>
    /// The mod asset title
    /// </summary>
    public required string Title { get; set; }

    /// <summary>
    /// The mod asset description
    /// </summary>
    public required string Description { get; set; }

    /// <summary>
    /// The mod asset's thumbnail URL
    /// </summary>
    public required string ThumbnailUrl { get; set; }

    /// <summary>
    /// The mod asset URL
    /// </summary>
    public required string Url { get; set; }
}

public class Mod
{
    /// <summary>
    /// The mod id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The game id this mod is for
    /// </summary>
    public required int GameId { get; set; }

    /// <summary>
    /// The name of the mod
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The mod slug that would appear in the URL
    /// </summary>
    public required string Slug { get; set; }

    /// <summary>
    /// Relevant links for the mod such as Issue tracker and Wiki
    /// </summary>
    public required ModLinks Links { get; set; }

    /// <summary>
    /// Mod summary
    /// </summary>
    public required string Summary { get; set; }

    /// <summary>
    /// Current mod status
    /// </summary>
    public required ModStatus Status { get; set; }

    /// <summary>
    /// Number of downloads for the mod
    /// </summary>
    public required long DownloadCount { get; set; }

    /// <summary>
    /// Whether the mod is included in the featured mods list
    /// </summary>
    public required bool IsFeatured { get; set; }

    /// <summary>
    /// The main category of the mod as it was chosen by the mod author
    /// </summary>
    public required int PrimaryCategoryId { get; set; }

    /// <summary>
    /// List of categories that this mod is related to
    /// </summary>
    public required List<Category> Categories { get; set; }

    /// <summary>
    /// The class id this mod belongs to
    /// </summary>
    public int? ClassId { get; set; }

    /// <summary>
    /// List of the mod's author
    /// </summary>
    public required List<ModAuthor> Authors { get; set; }

    /// <summary>
    /// The mod's logo asset
    /// </summary>
    public required ModAsset Logo { get; set; }

    /// <summary>
    /// List of screenshots assets
    /// </summary>
    public required List<ModAsset> Screenshots { get; set; }

    /// <summary>
    /// The id of the main file of the mod
    /// </summary>
    public required int MainFileId { get; set; }

    /// <summary>
    /// List of latest files of the mod
    /// </summary>
    public required List<File> LatestFiles { get; set; }

    /// <summary>
    /// List of file related details for the latest files of the mod
    /// </summary>
    public required List<FileIndex> LatestFilesIndexes { get; set; }

    /// <summary>
    /// List of file related details for the latest early access files of the mod
    /// </summary>
    public required List<FileIndex> LatestEarlyAccessFilesIndexes { get; set; }

    /// <summary>
    /// The creation date of the mod
    /// </summary>
    public required DateTime DateCreated { get; set; }

    /// <summary>
    /// The last time the mod was modified
    /// </summary>
    public required DateTime DateModified { get; set; }

    /// <summary>
    /// The release date of the mod
    /// </summary>
    public required DateTime DateReleased { get; set; }

    /// <summary>
    /// Is mod allowed to be distributed
    /// </summary>
    public bool? AllowModDistribution { get; set; }

    /// <summary>
    /// The mod popularity rank for the game
    /// </summary>
    public int GamePopularityRank { get; set; }

    /// <summary>
    /// Is the mod available for search. This can be false when a mod is experimental, in a deleted state or has only alpha files
    /// </summary>
    public required bool IsAvailable { get; set; }

    /// <summary>
    /// The mod's thumbs up count
    /// </summary>
    public required int ThumbsUpCount { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Id})";
    }
}

public class Pagination
{
    /// <summary>
    /// A zero based index of the first item that is included in the response
    /// </summary>
    public required int Index { get; set; }

    /// <summary>
    /// The requested number of items to be included in the response
    /// </summary>
    public required int PageSize { get; set; }

    /// <summary>
    /// The actual number of items that were included in the response
    /// </summary>
    public required long ResultCount { get; set; }

    /// <summary>
    /// The total number of items available by the request
    /// </summary>
    public required long TotalCount { get; set; }

    public override string ToString()
    {
        return $"{Index} / {TotalCount}";
    }
}

public class SearchModsResponse
{
    /// <summary>
    /// The response data
    /// </summary>
    public List<Mod> Data { get; set; } = [];

    /// <summary>
    /// The response pagination information
    /// </summary>
    public required Pagination Pagination { get; set; }
}
