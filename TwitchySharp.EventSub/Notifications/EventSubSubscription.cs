using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.EventSub.Notifications;
public record EventSubSubscription<TCondition> : EventSubSubscription
    where TCondition : class
{
    public new required TCondition Condition { get; init; }
}

public record EventSubSubscription
{
    public required string Id { get; init; }
    public required string Type { get; init; }
    public required string Version { get; init; }
    public required EventSubSubscriptionStatus Status { get; init; }
    public required int Cost { get; init; }
    public ImmutableDictionary<string, object>? Condition { get; init; }
    public required EventSubTransport Transport { get; init; }
    public required DateTimeOffset CreatedAt { get; init; }
}
