using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;
/// <summary>
/// The host preferences for Guest Star have been updated.
/// </summary>
/// <remarks>
/// Requires a user access token that includes (one of) <see cref="Scope.ChannelReadGuestStar"/>, <see cref="Scope.ChannelManageGuestStar"/>, <see cref="Scope.ModeratorReadGuestStar"/>, or <see cref="Scope.ModeratorManageGuestStar"/>.
/// </remarks>
/// <param name="BroadcasterUserId">The user id of the broadcaster (channel) hosting the Guest Star Session.</param>
/// <param name="ModeratorUserId">The user id of the broadcaster or a moderator of the specified broadcaster.</param>
public sealed record ChannelGuestStarSettingsUpdate(string BroadcasterUserId, string ModeratorUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_GUEST_STAR_SETTINGS_UPDATE;
    public string Version => EventSubSubscriptionTypeVersions.BETA;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId)
            .Set("moderator_user_id", ModeratorUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
