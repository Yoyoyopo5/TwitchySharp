using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Chat;
/// <summary>
/// A moderator or bot has cleared all messages from a specific user.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.UserReadChat"/>, or
/// an app access token where the <paramref name="UserId"/> has created a user access token with <see cref="Scope.UserBot"/> and <see cref="Scope.ChannelBot"/> (from broadcaster or moderator status).
/// The user who created the access token must be the same user as the <paramref name="UserId"/>.
/// </remarks>
/// <param name="BroadcasterUserId">User ID of the channel to receive chat clear user messages events for.</param>
/// <param name="UserId">The user ID to read chat as.</param>
public sealed record ChannelChatClearUserMessages(string BroadcasterUserId, string UserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_CHAT_CLEAR_USER_MESSAGES;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("user_id", UserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
