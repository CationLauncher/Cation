using Cation.Core.ModPlatform.CurseForge;
using Cation.Models.ModPlatform.CurseForge;

namespace Cation.Test.Core;

public class CurseForgeTest : CationTestBase
{
    [Test]
    public async Task GetModCategoriesTest()
    {
        var modCategories = await CurseForgeApi.GetCategories(ClassId.Mod);
        Assert.That(modCategories, Is.Not.Null);
        Assert.That(modCategories, Is.Not.Empty);

        var testForNull = await CurseForgeApi.GetCategories((ClassId)1234);
        Assert.That(testForNull, Is.Null);
    }

    [Test]
    public async Task SearchModsLegacyTest()
    {
        var response = await CurseForgeApi.SearchModsLegacyAsync(ClassId.Mod, "Create Ratatouille",
            ModsSearchSortFieldLegacy.Relevancy);
        Assert.That(response, Is.Not.Null);
        var mods = response.Data;
        Assert.That(mods, Is.Not.Empty);
        var mod = mods[0];
        Assert.That(mod.Name, Is.EqualTo("Create Ratatouille"));

        var testForEmpty = await CurseForgeApi.SearchModsLegacyAsync((ClassId)1234, "",
            ModsSearchSortFieldLegacy.Relevancy);
        Assert.That(testForEmpty, Is.Not.Null);
        Assert.That(testForEmpty.Data, Is.Empty);
    }
}
