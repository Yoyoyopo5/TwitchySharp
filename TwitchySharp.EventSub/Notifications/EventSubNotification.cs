using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Shared.EventSub.Models;

namespace TwitchySharp.EventSub.Notifications;
public record EventSubNotification<TEvent, TCondition> : IEventSubNotification
    where TEvent : class
    where TCondition : class
{
    public required EventSubSubscription<TCondition> Subscription { get; init; }
    EventSubSubscription IEventSubNotification.Subscription => Subscription;
    public required TEvent Event { get; init; }
}

public interface IEventSubNotification
{
    EventSubSubscription Subscription { get; }
}

internal record EventSubNotification(EventSubSubscriptionType Subscription);
