﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.SharedChat;
/// <summary>
/// A notification when a channel becomes active in an active shared chat session.
/// </summary>
/// <remarks>
/// No authorization required.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive shared chat session begin events for.</param>
public sealed record ChannelSharedChatSessionBegin(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_SHARED_CHAT_SESSION_BEGIN;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
