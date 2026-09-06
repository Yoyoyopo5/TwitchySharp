using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub;

/// <summary>
/// Indicates that a required condition key was missing in a <see cref="EventSubSubscription.Condition"/>.
/// </summary>
/// <param name="MissingKey">The missing required condition key.</param>
public record ConditionMissingRequiredKeyError(ConditionKey MissingKey)
    : Error("The condition was missing a required key");

/// <summary>
/// An <see cref="IEventSubSubscriptionTypeSpecification"/> that can be created via a <see cref="EventSubSubscription.Condition"/>.
/// </summary>
/// <typeparam name="T">The <see cref="IEventSubSubscriptionTypeSpecification"/> type.</typeparam>
public interface IConditionConstructable<T>
    where T : EventSubSubscriptionTypeSpecification
{
    static abstract EventSubSubscriptionType SubscriptionType { get; }
    static abstract Validation<T> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition);
}

/// <summary>
/// An EventSub subscription type.
/// </summary>
public abstract record EventSubSubscriptionTypeSpecification
{
    /// <summary>
    /// The type of the subscription, combining name and version.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </remarks>
    public abstract EventSubSubscriptionType Type { get; }
    /// <summary>
    /// A dictionary that contains the parameter values that are specific to the specified subscription type.
    /// For the object's required and optional fields, see the subscription type's documentation.
    /// </summary>
    public abstract IReadOnlyDictionary<ConditionKey, object> Condition { get; }

    /// <summary>
    /// The authentication context that requests using this specification should be made under.
    /// </summary>
    public abstract EventSubSubscriptionAuthenticationContext AuthenticationContext { get; }
}

internal static class EventSubSubscriptionTypeSpecificationExtensions
{
    extension(EventSubSubscriptionTypeSpecification specification)
    {
        public ITwitchRequestAuthenticationContext<TwitchIdentity> ToRequestAuthenticationContext(EventSubTransportMethod transportMethod)
            => specification.AuthenticationContext switch
            {
                EventSubSubscriptionAuthenticationContext.ClientAuthorized clientContext => clientContext.ToClientAuthenticationContext(),
                EventSubSubscriptionAuthenticationContext.UserAuthorized userContext => transportMethod switch
                {
                    _ when transportMethod == EventSubTransportMethod.Websocket => userContext.ToUserWithScopesAuthenticationContext(),
                    _ => userContext.ToUserSupportingPriorAuthorizationAuthenticationContext(true)
                },
                _ => TwitchRequestAuthenticationContext.Default
            };
    }
}
