using Cation.Core.Network;
using Cation.Models.ModPlatform.CurseForge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cation.Core.ModPlatform.CurseForge;

public static class CurseForgeApi
{
    private const int GameId = 432;

    public static async Task<List<Category>?> GetCategories(ClassId classId)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("CurseForgeClient");
        await using var stream =
            await httpClient.GetStreamOrNullAsync($"/v1/categories?gameId={GameId}&classId={(int)classId}");
        if (stream == null)
            return null;
        var response = await CurseForgeJsonContext.DeserializeAsync<GetCategoriesResponse>(stream);
        return response?.Data;
    }

    public static async Task<SearchModsResponse?> SearchModsAsync(ClassId classId, string searchFilter,
        ModsSearchSortField sortField, bool sortDesc = true, int index = 0, int pageSize = 50)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("CurseForgeClient");
        var sortOrder = sortDesc ? "desc" : "asc";
        await using var stream = await httpClient.GetStreamOrNullAsync(
            $"/v1/mods/search?gameId={GameId}&classId={(int)classId}&searchFilter={searchFilter}&sortField={(int)sortField}&sortOrder={sortOrder}&index={index}&pageSize={pageSize}");
        if (stream == null)
            return null;
        return await CurseForgeJsonContext.DeserializeAsync<SearchModsResponse>(stream);
    }
}
