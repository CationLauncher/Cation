using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Cation.Core.Network;

public static class HttpClientExtension
{
    public static void SetupHttpClient(this HttpClient client, Dictionary<string, string>? header = null)
    {
        client.DefaultRequestHeaders.UserAgent.ParseAdd("Cation/1.0");
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        header?.ToList().ForEach(pair => client.DefaultRequestHeaders.Add(pair.Key, pair.Value));
    }
}
