using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api;
/// <summary>
/// A Twitch request that requires the ClientId and Authorization headers to be set.
/// </summary>
public interface IRequireAuthorization
{
    /// <summary>
    /// The identity to use for the request.
    /// </summary>
    public TwitchApiIdentity Identity { get; }
    /// <summary>
    /// One of these user scopes is required. 
    /// </summary>
    public IEnumerable<Scope> ValidScopes { get; }
    /// <summary>
    /// Allows for manually setting an access token.
    /// </summary>
    /// <remarks>
    /// This property should override all other access token configuration
    /// and be guaranteed to be the bearer authorization used for the request.
    /// </remarks>
    public AccessToken? OverrideAccessToken { get; }
}

internal record AuthorizationRequirement // Need this to assign a ClientIdentity after the request initialized.
    : IRequireAuthorization
{
    public static AuthorizationRequirement FromRequest(IRequireAuthorization request)
        => new()
        {
            Identity = request.Identity,
            ValidScopes = request.ValidScopes,
            OverrideAccessToken = request.OverrideAccessToken,
        };
    public AuthorizationRequirement WithClientFallback(ClientIdentity? client)
        => this with { Identity = Identity.WithFallbackClient(client) };
    public required TwitchApiIdentity Identity { get; init; }
    public required IEnumerable<Scope> ValidScopes { get; init; }
    public AccessToken? OverrideAccessToken { get; init; }
}
