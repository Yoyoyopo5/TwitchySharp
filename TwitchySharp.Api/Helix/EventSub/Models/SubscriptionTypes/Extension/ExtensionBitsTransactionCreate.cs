using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// A Bits transaction occurred for a specified Twitch Extension.
/// </summary>
/// <remarks>
/// <b>Note:</b> This subscription type is only supported by the webhooks transport. It cannot be used with WebSockets.
/// Requires an app access token created by the <paramref name="ExtensionClientId"/>.
/// </remarks>
/// <param name="ExtensionClientId">The client id of the extension.</param>
public sealed record ExtensionBitsTransactionCreate(string ExtensionClientId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.EXTENSION_BITS_TRANSACTION_CREATE;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("extension_client_id", ExtensionClientId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
