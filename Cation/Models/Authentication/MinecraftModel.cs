using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cation.Models.Authentication;

public class AuthenticationLoginWithXboxRequest
{
    /// <summary>
    /// Identity token. The value should be <c>XBL3.0 x=&lt;User hashcode&gt;;&lt;XSTS access token&gt;</c>.
    /// </summary>
    [JsonPropertyName("identityToken")]
    public required string IdentityToken { get; set; }
}

public class AuthenticationLoginWithXboxResponse
{
    /// <summary>
    /// UUID (not the UUID for the player)
    /// </summary>
    public required string Username { get; set; }

    /// <summary>
    /// Minecraft access token.
    /// </summary>
    public required string AccessToken { get; set; }

    /// <summary>
    /// Token type. This is always <c>Bearer</c>.
    /// </summary>
    public required string TokenType { get; set; }

    /// <summary>
    /// Time period until the token expires in seconds.
    /// </summary>
    public required int ExpiresIn { get; set; }
}

public class MinecraftProfileResponse
{
    /// <summary>
    /// Player's UUID.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Player name.
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// A list of info of all the skins the player owns.
    /// </summary>
    public required List<Skin> Skins { get; set; }

    /// <summary>
    /// A list of info of all the skins the player owns.
    /// </summary>
    public required List<Cape> Capes { get; set; }


    public class Skin
    {
        /// <summary>
        /// Skin's UUID.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Usage status for the skin.
        /// </summary>
        public required string State { get; set; }

        /// <summary>
        /// URL to the skin.
        /// </summary>
        public required string Url { get; set; }

        /// <summary>
        /// Skin variant. <c>CLASSIC</c> for the Steve model and <c>SLIM</c> for the Alex model.
        /// </summary>
        public required string Variant { get; set; }
    }

    public class Cape
    {
        /// <summary>
        /// Cape's UUID.
        /// </summary>
        public required string Id { get; set; }

        /// <summary>
        /// Usage status for the cape.
        /// </summary>
        public required string State { get; set; }

        /// <summary>
        /// URL to the cape.
        /// </summary>
        public required string Url { get; set; }

        /// <summary>
        /// Alias for the cape.
        /// </summary>
        public required string Alias { get; set; }
    }
}
