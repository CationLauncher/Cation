using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cation.Models.Authentication;

public class UserAuthenticateRequest
{
    /// <summary>
    /// Authentication properties
    /// </summary>
    public required UserAuthenticateRequestPropertiesInfo Properties { get; set; }

    /// <summary>
    /// Replying party. This should be <c>http://auth.xboxlive.com</c>.
    /// </summary>
    public required string RelyingParty { get; set; }

    /// <summary>
    /// Type of the access token. This should be <c>JWT</c>.
    /// </summary>
    public required string TokenType { get; set; }
}

public class UserAuthenticateRequestPropertiesInfo
{
    /// <summary>
    /// Login method. This should be <c>RPS</c>.
    /// </summary>
    public required string AuthMethod { get; set; }

    /// <summary>
    /// Website name. This should be <c>user.auth.xboxlive.com</c>.
    /// </summary>
    public required string SiteName { get; set; }

    /// <summary>
    /// Ticket used for logging in. Value should be <c>d=&lt;Microsoft access token&gt;</c>.
    /// </summary>
    public required string RpsTicket { get; set; }
}

public class UserAuthenticateResponse
{
    /// <summary>
    /// Time when obtaining the Xbox Live token.
    /// </summary>
    public required DateTime IssueInstant { get; set; }

    /// <summary>
    /// Time the Xbox Live token is expired.
    /// </summary>
    public required DateTime NotAfter { get; set; }

    /// <summary>
    /// Xbox Live access token.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Unknown.
    /// </summary>
    public required XboxLiveDisplayClaimsInfo DisplayClaims { get; set; }
}

public class XstsAuthorizeRequest
{
    /// <summary>
    /// Auth properties
    /// </summary>
    public required XstsAuthorizeRequestPropertiesInfo Properties { get; set; }

    /// <summary>
    /// Replying party. This should be <c>rp://api.minecraftservices.com/</c>.
    /// </summary>
    public required string RelyingParty { get; set; }

    /// <summary>
    /// Type of the access token. This should be <c>JWT</c>.
    /// </summary>
    public required string TokenType { get; set; }
}

public class XstsAuthorizeRequestPropertiesInfo
{
    /// <summary>
    /// Sandbox ID. This should be <c>RETAIL</c>.
    /// </summary>
    public required string SandboxId { get; set; }

    /// <summary>
    /// User's Xbox Live token.
    /// </summary>
    public required List<string> UserTokens { get; set; }
}

public class XstsAuthorizeResponse
{
    /// <summary>
    /// Time when obtaining the XSTS token.
    /// </summary>
    public required DateTime IssueInstant { get; set; }

    /// <summary>
    /// Time the XSTS token is expired.
    /// </summary>
    public required DateTime NotAfter { get; set; }

    /// <summary>
    /// XSTS token.
    /// </summary>
    public required string Token { get; set; }

    /// <summary>
    /// Unknown.
    /// </summary>
    public required XboxLiveDisplayClaimsInfo DisplayClaims { get; set; }
}

public class XboxLiveDisplayClaimsInfo
{
    /// <summary>
    /// Unknown.
    /// </summary>
    [JsonPropertyName("xui")]
    public required List<XboxLiveXuiInfo> Xui { get; set; }
}

public class XboxLiveXuiInfo
{
    /// <summary>
    /// User hashcode.
    /// </summary>
    [JsonPropertyName("uhs")]
    public required string Uhs { get; set; }
}
