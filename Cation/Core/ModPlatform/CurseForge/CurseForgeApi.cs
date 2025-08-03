using Cation.Core.Network;
using Cation.Models.ModPlatform.CurseForge;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cation.Core.ModPlatform.CurseForge;

public static class CurseForgeApi
{
    private const int GameId = 432;
    private const string LegacyBaseUrl = "https://www.curseforge.com/api/v1";

    public static async Task<List<Category>?> GetCategories(ClassId classId)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("CurseForgeClient");
        await using var stream = await httpClient.GetStreamOrNullAsync($"/v1/categories?gameId={GameId}&classId={(int)classId}");
        if (stream == null)
            return null;
        var response = await CurseForgeJsonContext.DeserializeAsync<GetCategoriesResponse>(stream);
        return response?.Data;
    }

    public static async Task<SearchModsLegacyResponse?> SearchModsLegacyAsync(ClassId classId, string filterText, ModsSearchSortFieldLegacy sortField, int index = 0, int pageSize = 50)
    {
        using var httpClient = App.HttpClientFactory.CreateClient("CurseForgeClient");
        await using var stream = await httpClient.GetStreamOrNullAsync($"{LegacyBaseUrl}/mods/search?gameId={GameId}&classId={(int)classId}&filterText={filterText}&sortField={(int)sortField}&index={index}&pageSize={pageSize}");
        if (stream == null)
            return null;
        return await CurseForgeJsonContext.DeserializeAsync<SearchModsLegacyResponse>(stream);
    }
}
