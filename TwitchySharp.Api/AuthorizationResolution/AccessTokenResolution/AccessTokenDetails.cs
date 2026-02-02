using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api;

/// <summary>
/// Contains details about a specific access token.
/// </summary>
/// <remarks>
/// 
/// </remarks>
public abstract record AccessTokenDetails
{
    public required DateTimeOffset ExpiresAt { get; init; }
}

public record AppAccessTokenDetails
    : AccessTokenDetails
{
    public required ClientIdentity Client { get; init; }
    public required AppAccessToken AccessToken { get; init; }
}

public record ExtensionJwtDetails
    : AccessTokenDetails
{
    public required ExtensionIdentity Extension { get; init; }
    public required ExtensionJsonWebToken JsonWebToken { get; init; }
}
