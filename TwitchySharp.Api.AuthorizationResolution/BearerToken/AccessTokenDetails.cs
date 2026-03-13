using System.Collections.Immutable;
using System.Security.AccessControl;

namespace TwitchySharp.Api.AuthorizationResolution;
/// <summary>
/// Contains information about a specific access token and the context it belongs to.
/// </summary>
public abstract partial record AccessTokenDetails
{
    /// <summary>
    /// The identity associated with the access token.
    /// </summary>
    public TwitchIdentity Identity => BaseIdentity;
    protected abstract TwitchIdentity BaseIdentity { get; }
    /// <summary>
    /// The access token.
    /// </summary>
    public IAccessToken AccessToken => BaseAccessToken;
    protected abstract IAccessToken BaseAccessToken { get; }
    /// <summary>
    /// The date and time when the access token expires.
    /// </summary>
    public DateTimeOffset ExpiresAt { get; init; }
}