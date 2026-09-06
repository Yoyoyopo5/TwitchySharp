using TwitchySharp.Infrastructure.Functional;

namespace TwitchySharp.Api.Helix.EventSub.Subscriptions;
/// <summary>
/// Sends a notification when EventSub disables a shard due to the status of the underlying transport changing.
/// </summary>
/// <remarks>
/// Requires an app access token where the client id used to create the token is the same client id in the condition.
/// If a <paramref name="ConduitId"/> is specified, the client id must be the owner of the conduit.
/// </remarks>
/// <param name="ClientId">
/// The client id of the application to get conduit disabled notifications for.
/// This application must have created the app access token used to make the request.
/// </param>
/// <param name="ConduitId">
/// The conduit ID to receive events for.
/// If <see langword="null"/>, events for all of this client's conduits are sent.</param>
public sealed record ConduitShardDisabled(ClientId ClientId, ConduitId? ConduitId = null)
    : EventSubSubscriptionTypeSpecification, IConditionConstructable<ConduitShardDisabled>
{
    public override EventSubSubscriptionType Type => EventSubSubscriptionType.ConduitShardDisabled;
    public static EventSubSubscriptionType SubscriptionType => EventSubSubscriptionType.ConduitShardDisabled;
    public override EventSubSubscriptionAuthenticationContext.ClientAuthorized AuthenticationContext { get; }
        = new() { Identity = new TwitchIdentity.Client(ClientId) };
    public override IReadOnlyDictionary<ConditionKey, object> Condition { get; }
        = new EventSubSubscriptionCondition()
            .Set(new("client_id"), ClientId)
            .Set(new("conduit_id"), ConduitId);

    public static Validation<ConduitShardDisabled> FromCondition(IReadOnlyDictionary<ConditionKey, string> condition)
        => condition
            .GetRequiredValue(new("client_id"), out ClientId clientId, value => new(value))
            .GetValue(new("conduit_id"), out ConduitId conduitId, value => new(value))
            .Map(_ => new ConduitShardDisabled(clientId, conduitId));
}
