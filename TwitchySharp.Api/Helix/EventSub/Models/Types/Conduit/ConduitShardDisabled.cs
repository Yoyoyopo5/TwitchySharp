using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Conduit;
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
/// If <see langword="null"/>, events for all of this client’s conduits are sent.</param>
public sealed record ConduitShardDisabled(string ClientId, string? ConduitId = null)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CONDUIT_SHARD_DISABLED;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("client_id", ClientId)
            .Set("conduit_id", ConduitId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
