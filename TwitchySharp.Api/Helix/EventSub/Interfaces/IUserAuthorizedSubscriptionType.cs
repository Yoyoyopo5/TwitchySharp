using System.Collections.Generic;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// An EventSub subscription type that requires user authorization.
/// </summary>
/// <remarks>
/// These subscription types require a user access token with the specified scopes.
/// The authorizing user must match the condition key specified by <see cref="AuthorizingUserConditionKey"/>.
/// </remarks>
public interface IUserAuthorizedSubscriptionType : IEventSubSubscriptionType
{
    /// <summary>
    /// The condition key that identifies which user must authorize this subscription.
    /// </summary>
    ConditionKey AuthorizingUserConditionKey { get; }

    /// <summary>
    /// The scopes required for user authorization.
    /// </summary>
    IReadOnlySet<Scope> ValidScopes { get; }
}

internal static class UserAuthorizedSubscriptionTypeExtensions
{
    /// <summary>
    /// Gets the authorizing user identity from the subscription type's condition.
    /// </summary>
    /// <param name="subscriptionType">The subscription type to get the authorizing user from.</param>
    /// <returns>
    /// A <see cref="TwitchIdentity.User"/> for the authorizing user, or <see langword="null"/>
    /// if the condition key is not found in the subscription's condition.
    /// </returns>
    internal static TwitchIdentity.User? GetAuthorizingUser(this IUserAuthorizedSubscriptionType subscriptionType)
    {
        var conditionKey = subscriptionType.AuthorizingUserConditionKey;
        if (!subscriptionType.Condition.TryGetValue(conditionKey, out object? value))
            return null;
        if (value is not UserId userId)
            return null;
        return new TwitchIdentity.User(userId);
    }
}
