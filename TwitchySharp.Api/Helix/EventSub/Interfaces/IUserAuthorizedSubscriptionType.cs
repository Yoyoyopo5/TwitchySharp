using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub;

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
    IEnumerable<Scope> ValidScopes { get; }
}
