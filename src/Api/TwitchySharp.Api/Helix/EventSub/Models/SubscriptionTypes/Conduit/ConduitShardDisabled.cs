using System.Collections.Generic;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
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
    : IEventSubSubscriptionType
{
    public EventSubSubscriptionType Type => EventSubSubscriptionType.ConduitShardDisabled;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set(new ConditionKey("client_id"), ClientId)
            .Set(new ConditionKey("conduit_id"), ConduitId);
    public IReadOnlyDictionary<ConditionKey, object> Condition => _condition;
}
