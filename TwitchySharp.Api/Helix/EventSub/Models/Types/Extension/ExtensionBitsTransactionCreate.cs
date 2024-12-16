using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Extension;
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

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("extension_client_id", ExtensionClientId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
