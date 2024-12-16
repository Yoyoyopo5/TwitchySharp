using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Chat;
/// <summary>
/// A user is notified if their message is caught by automod.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/>.
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) to receive chat message events for.</param>
/// <param name="UserId">The user id of the user to read chat as.</param>
public sealed record ChannelChatUserMessageHold(string BroadcasterUserId, string UserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_CHAT_USER_MESSAGE_HOLD;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("user_id", UserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
