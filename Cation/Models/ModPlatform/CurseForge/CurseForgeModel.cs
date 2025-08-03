using Cation.Core.ModPlatform.CurseForge;
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

public class FileIndexLegacy
{
    /// <summary>
    /// The file id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The file name
    /// </summary>
    public string? FileName { get; set; }

    /// <summary>
    /// The file display name
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    /// The compatible game versions for this file
    /// </summary>
    public required List<string> GameVersions { get; set; }

    /// <summary>
    /// Game version type id
    /// </summary>
    public required List<int> GameVersionTypeIds { get; set; }

    public DateTime? DateCreated { get; set; }

    public DateTime? DateModified { get; set; }

    public FileReleaseType? ReleaseType { get; set; }

    public bool? IsEarlyAccessContent { get; set; }

    public DateTime? EarlyAccessEndDate { get; set; }

    public override string ToString()
    {
        return FileName != null ? $"{FileName} ({Id})" : $"{Id}";
    }
}

public class FileLegacy
{
    public required GameVersion GameVersion { get; set; }
    public required List<FileIndexLegacy> Files { get; set; }
}

public class GameVersion
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Slug { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Id})";
    }
}

public class GetCategoriesResponse
{
    /// <summary>
    /// The response data
    /// </summary>
    public required List<Category> Data { get; set; }
}

public class ModAuthorLegacy
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
    /// The mod author username
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Is early access author
    /// </summary>
    public required bool IsEarlyAccessAuthor { get; set; }
}

public class ModLegacy
{
    /// <summary>
    /// The mod id
    /// </summary>
    public required int Id { get; set; }

    /// <summary>
    /// The name of the mod
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// The mod slug that would appear in the URL
    /// </summary>
    public required string Slug { get; set; }

    /// <summary>
    /// Mod summary
    /// </summary>
    public required string Summary { get; set; }

    /// <summary>
    /// The game version that this mod is compatible with
    /// </summary>
    public required string GameVersion { get; set; }

    /// <summary>
    /// Current mod status
    /// </summary>
    public required ModStatus Status { get; set; }

    /// <summary>
    /// Number of downloads for the mod
    /// </summary>
    public required long Downloads { get; set; }

    /// <summary>
    /// The class this mod belongs to
    /// </summary>
    public required Category Class { get; set; }

    /// <summary>
    /// List of categories that this mod is related to
    /// </summary>
    public required List<Category> Categories { get; set; }

    /// <summary>
    /// List of the mod's author
    /// </summary>
    public required ModAuthorLegacy Author { get; set; }

    /// <summary>
    /// The creation timestamp of the mod
    /// </summary>
    public required long CreationDate { get; set; }

    /// <summary>
    /// The creation date of the mod
    /// </summary>
    [JsonIgnore]
    public DateTime CreationDateTime => DateTimeOffset.FromUnixTimeSeconds(CreationDate).DateTime;

    /// <summary>
    /// The update timestamp of the mod
    /// </summary>
    public required long UpdateDate { get; set; }

    /// <summary>
    /// The update date of the mod
    /// </summary>
    [JsonIgnore]
    public DateTime UpdateDateTime => DateTimeOffset.FromUnixTimeSeconds(UpdateDate).DateTime;

    /// <summary>
    /// The release timestamp of the mod
    /// </summary>
    public required long ReleaseDate { get; set; }

    /// <summary>
    /// The release date of the mod
    /// </summary>
    [JsonIgnore]
    public DateTime ReleaseDateTime => DateTimeOffset.FromUnixTimeSeconds(ReleaseDate).DateTime;

    /// <summary>
    /// The avatar URL of the mod
    /// </summary>
    public required string AvatarUrl { get; set; }

    /// <summary>
    /// The thumbnail URL of the mod
    /// </summary>
    public required string ThumbnailUrl { get; set; }

    /// <summary>
    /// The file size of the mod
    /// </summary>
    public required long FileSize { get; set; }

    public required bool IsClientCompatible { get; set; }
    public required FileIndexLegacy LatestFileDetails { get; set; }
    public required bool HasEarlyAccessFiles { get; set; }
    public required bool HasLocalization { get; set; }
    public required List<FileLegacy> WebsiteRecentFiles { get; set; }
    public required bool IsMainFileClientCompatible { get; set; }
    public required bool IsPremium { get; set; }
    public required bool IsAvailableForDownload { get; set; }
    public required int DownloadAvailability { get; set; }

    public override string ToString()
    {
        return $"{Name} ({Id})";
    }
}

public class PaginationLegacy
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
    /// The total number of items available by the request
    /// </summary>
    public required long TotalCount { get; set; }

    public override string ToString()
    {
        return $"{Index} / {TotalCount}";
    }
}

public class SearchModsLegacyResponse
{
    /// <summary>
    /// The response data
    /// </summary>
    public List<ModLegacy> Data { get; set; } = [];

    /// <summary>
    /// The response pagination information
    /// </summary>
    public required PaginationLegacy Pagination { get; set; }
}
