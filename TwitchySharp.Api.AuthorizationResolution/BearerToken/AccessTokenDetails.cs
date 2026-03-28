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
    public DateTimeOffset ExpiresAt => BaseExpiresAt;
    protected abstract DateTimeOffset BaseExpiresAt { get; }
}

public static partial class AccessTokenDetailsEnumerableExtensions
{
    public static IEnumerable<TDetails> WhereTokenMeetsRequirements<TDetails>(
        this IEnumerable<AccessTokenDetails> tokens,
        TwitchRequestAuthorizationContext context)
        where TDetails : AccessTokenDetails
        => tokens.WhereTokenMeetsRequirements(context).OfType<TDetails>();

    public static IEnumerable<AccessTokenDetails> WhereTokenMeetsRequirements(
        this IEnumerable<AccessTokenDetails> tokens,
        TwitchRequestAuthorizationContext context)
        => context.Identity switch
        {
            TwitchIdentity.Client => tokens
                .OfType<AccessTokenDetails.App>()
                .WhereTokenMeetsRequirements(context),
            TwitchIdentity.User => tokens
                .OfType<AccessTokenDetails.User>()
                .WhereTokenMeetsRequirements(context),
            TwitchIdentity.Extension => tokens
                .OfType<AccessTokenDetails.App>()
                .WhereTokenMeetsRequirements(context),
            _ => tokens.Where(t => t.Identity == context.Identity)
        };
}