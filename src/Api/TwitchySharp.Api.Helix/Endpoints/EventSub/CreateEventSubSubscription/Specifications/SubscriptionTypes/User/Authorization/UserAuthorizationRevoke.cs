using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// A user's authorization has been revoked for your client id.
/// Use this webhook to meet government requirements for handling user data, such as GDPR, LGPD, or CCPA.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported with the webhook transport. It cannot be used with WebSockets.
/// Requires an app access token created by the same client id as the <paramref name="ClientId"/> parameter.
/// </remarks>
/// <param name="ClientId">
/// The client id of the application to get authorization revocation notifications for.
/// This must match the client id in the application access token used to make the request.
/// </param>
public sealed record UserAuthorizationRevoke(ClientId ClientId)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<UserAuthorizationRevoke>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.UserAuthorizationRevoke;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.UserAuthorizationRevoke;
    public override EventSubSubscriptionAuthenticationContext.ClientAuthorized AuthenticationContext { get; }
        = new() { Identity = new TwitchIdentity.Client(ClientId) };
    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("client_id"), ClientId);
    public static Validation<UserAuthorizationRevoke> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("client_id"), out ClientId ClientId, value => new(value))
            .Map(_ => new UserAuthorizationRevoke(ClientId));
}
