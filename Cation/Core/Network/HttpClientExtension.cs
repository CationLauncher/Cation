using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Cation.Core.Network;

public static class HttpClientExtension
{
    public static void SetupHttpClient(this HttpClient client, Dictionary<string, string>? header = null)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Cation/1.0");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        header?.ToList().ForEach(pair => client.DefaultRequestHeaders.Add(pair.Key, pair.Value));
    }

    public static async Task<Stream?> GetStreamOrNullAsync(this HttpClient client, [StringSyntax("Uri")] string? requestUri)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, requestUri);
        var responseMessage = await client.SendAsync(request);
        if (!responseMessage.IsSuccessStatusCode)
            return null;
        return await responseMessage.Content.ReadAsStreamAsync();
    }
}
