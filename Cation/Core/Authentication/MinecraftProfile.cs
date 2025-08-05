namespace Cation.Core.Authentication;

public class MinecraftProfile(string id, string username, string accessToken)
{
    public string Id { get; init; } = id;
    public string Username { get; init; } = username;
    public string AccessToken { get; init; } = accessToken;
}
