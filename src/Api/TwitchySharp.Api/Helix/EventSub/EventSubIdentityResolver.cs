using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
using TwitchySharp.Shared.EventSub;
using TwitchySharp.Shared.EventSub.Enums;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;

public static class EventSubIdentityResolver
{
    /// <summary>
    /// Lookup map for the <see cref="ConditionKey"/> pointing to the user that requires authoriziation in an <see cref="EventSubSubscriptionCondition"/>.
    /// </summary>
    private static ImmutableDictionary<EventSubSubscriptionType, ConditionKey> AuthorizingUserConditionKeys { get; }
        = UserAuthorizedSubscriptionTypes
            .Select(RuntimeHelpers.GetUninitializedObject)
            .OfType<IUserAuthorizedSubscriptionType>()
            .ToImmutableDictionary(st => st.Type, st => st.AuthorizingUserConditionKey);

    private static IEnumerable<Type> UserAuthorizedSubscriptionTypes =>
        typeof(IUserAuthorizedSubscriptionType).Assembly
            .GetTypes()
            .Where(t =>
                t.Namespace == typeof(UserUpdate).Namespace // All subscription types should be in same namespace.
                && t.IsClass
                && !t.IsAbstract
                && !t.IsGenericTypeDefinition
                && typeof(IUserAuthorizedSubscriptionType).IsAssignableFrom(t)
                );

    /// <summary>
    /// Gets the authorizing user identity from an EventSub subscription, if one exists.
    /// </summary>
    /// <remarks>
    /// This is resolved from the <see cref="EventSubSubscription.Condition"/> on a subscription type basis.
    /// </remarks>
    /// <param name="subscription">The subscription to get the authorizing user for.</param>
    /// <returns>A <see cref="TwitchIdentity.User"/> indicating the Twitch user that authorized the subscription, if applicable.</returns>
    public static TwitchIdentity.User? GetAuthorizingUser(this EventSubSubscription subscription)
        => AuthorizingUserConditionKeys.TryGetValue(subscription.GetSubscriptionType(), out ConditionKey key) switch
        {
            true => subscription.Condition.TryGetValue(key, out string? userId) switch
            {
                true => new TwitchIdentity.User(new UserId(userId)),
                false => null
            },
            false => null
        };
}
