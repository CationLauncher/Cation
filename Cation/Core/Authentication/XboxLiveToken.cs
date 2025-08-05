namespace Cation.Core.Authentication;

public class XboxLiveToken(string userHash, string token)
{
    public string UserHash { get; init; } = userHash;
    public string Token { get; init;  } = token;
}
