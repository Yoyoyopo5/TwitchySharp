using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Webhooks;
public record EventSubWebhookRequestHeader
{
    public required string TwitchEventsubMessageId { get; init; }
    public required string TwitchEventsubMessageRetry { get; init; }
    public required string TwitchEventsubMessageType { get; init; }
    public required string TwitchEventsubMessageSignature { get; init; }
    public required string TwitchEventsubMessageTimestamp { get; init; }
    public required string TwitchEventsubSubscriptionType { get; init; }
    public required string TwitchEventsubSubscriptionVersion { get; init; }
}