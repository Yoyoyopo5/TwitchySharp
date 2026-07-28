using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user's authorization has been granted to your client id.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported with the webhook transport. It cannot be used with WebSockets.
/// Requires an app access token created by the same client id as the <paramref name="ClientId"/> parameter.
/// </remarks>
/// <param name="ClientId">
/// The client id of the application to get authorization grant notifications for.
/// This must match the client id in the application access token used to make the request.
/// </param>
public sealed record UserAuthorizationGrant(ClientId ClientId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<UserAuthorizationGrant>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.UserAuthorizationGrant;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.UserAuthorizationGrant;
    public override TwitchIdentity Identity { get; } = new TwitchIdentity.Client(ClientId);
    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("client_id"), ClientId);
    public static Validation<UserAuthorizationGrant> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("client_id"), out ClientId ClientId, value => new(value))
            .Map(_ => new UserAuthorizationGrant(ClientId));
}
