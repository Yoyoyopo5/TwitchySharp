using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// Base abstract record for access token details.
/// </summary>
/// <remarks>
/// See <see cref="UserAccessTokenDetails"/>, <see cref="AppAccessTokenDetails"/>, and <see cref="ExtensionJsonWebToken"/>.
/// </remarks>
public abstract record AccessTokenDetails<TIdentity, TToken>
    where TIdentity : TwitchApiIdentity
    where TToken: AccessToken
{
    public abstract TIdentity Identity { get; init; }
    public abstract TToken AccessToken { get; init; }
    public required DateTimeOffset ExpiresAt { get; init; }
}