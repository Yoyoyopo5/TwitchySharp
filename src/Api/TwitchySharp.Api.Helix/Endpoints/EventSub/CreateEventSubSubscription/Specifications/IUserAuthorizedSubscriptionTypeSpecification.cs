namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub subscription type that requires user authorization.
/// </summary>
/// <remarks>
/// These subscription types require a user access token with the specified scopes.
/// The authorizing user must match the condition key specified by <see cref="AuthorizingUserConditionKey"/>.
/// </remarks>
public interface IUserAuthorizedSubscriptionTypeSpecification : IEventSubSubscriptionTypeSpecification
{
    /// <summary>
    /// The condition key that identifies which user must authorize this subscription.
    /// </summary>
    internal static abstract ConditionKey AuthorizingUserConditionKey { get; }

    /// <summary>
    /// The user that must authorize this subscription.
    /// </summary>
    UserId AuthorizingUser { get; }

    /// <summary>
    /// The scopes required for user authorization.
    /// </summary>
    IReadOnlySet<Scope> ValidScopes { get; }
}
