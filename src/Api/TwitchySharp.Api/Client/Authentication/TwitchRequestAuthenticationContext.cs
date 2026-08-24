using System.Collections.Immutable;

namespace TwitchySharp.Api;

/// <summary>
/// The authentication context a Twitch request is to be made under.
/// </summary>
public interface ITwitchRequestAuthenticationContext<out TIdentity>
    where TIdentity : TwitchIdentity
{
    /// <summary>
    /// The identity to use for the request.
    /// </summary>
    TIdentity Identity { get; }
    /// <summary>
    /// Allows for manually setting an access token.
    /// </summary>
    /// <remarks>
    /// This property should override all other access token configuration
    /// and be guaranteed to be the bearer authorization used for the request.
    /// </remarks>
    BearerToken? BearerToken { get; }
}

/// <summary>
/// A basic authentication context with a <typeparamref name="TIdentity"/> identity.
/// </summary>
/// <typeparam name="TIdentity">The type of identity to use for the request.</typeparam>
public record TwitchRequestAuthenticationContext<TIdentity>
    : ITwitchRequestAuthenticationContext<TIdentity>
    where TIdentity : TwitchIdentity
{
    /// <summary>
    /// The bearer token to use for the request.
    /// </summary>
    /// <remarks>
    /// Typically, bearer tokens should be resolved via the <see cref="TwitchClient"/>
    /// based on the request identity. This can be used to set a definite bearer token
    /// for an individual request.
    /// </remarks>
    public BearerToken? BearerToken { get; init; }
    /// <summary>
    /// The identity to use for the request.
    /// </summary>
    /// <remarks>
    /// This is used by various <see cref="ClientId"/> and <see cref="Api.BearerToken"/>
    /// resolvers in the <see cref="TwitchClient"/>.
    /// </remarks>
    public required TIdentity Identity { get; init; }
}

/// <summary>
/// A <see cref="TwitchIdentity.User"/> authentication context that requires a specific <see cref="Scope"/>.
/// </summary>
public record UserWithScopesAuthenticationContext
    : IHaveScopes
{
    public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;
}

/// <summary>
/// A <see cref="TwitchIdentity.User"/> authentication context that supports
/// app access token authentication via a prior authorization.
/// </summary>
/// <remarks>
/// See my <see href="https://discuss.dev.twitch.com/t/questions-about-the-new-api-app-access-token-support-update/64655">Twitch Developers Forum post</see> for more information.
/// </remarks>
public record UserSupportingPriorAuthorizationAuthenticationContext
    : IHaveScopes, ISupportPriorAuthorization
{
    public IReadOnlySet<Scope> ValidScopes { get; init; } = ImmutableHashSet<Scope>.Empty;
    public bool UsePriorAuthorization { get; init; } = false;
}

/// <summary>
/// An authentication context requiring scopes.
/// </summary>
public interface IHaveScopes
{
    /// <summary>
    /// One of these user scopes is required.
    /// </summary>
    IReadOnlySet<Scope> ValidScopes { get; }
}

/// <summary>
/// An authentication context supporting app access tokens for user-authenticated endpoints.
/// </summary>
public interface ISupportPriorAuthorization
{
    /// <summary>
    /// Request to use an app access token as the request <see cref="BearerToken"/>
    /// in lieu of a user access token.
    /// </summary>
    /// <remarks>
    /// The client that created the app access token must have a prior authorization
    /// for the user the request is being made on behalf of.
    /// </remarks>
    bool UsePriorAuthorization { get; }
}
