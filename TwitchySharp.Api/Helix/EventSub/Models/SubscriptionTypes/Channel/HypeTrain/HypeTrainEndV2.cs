using System.Collections.Generic;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Shared.EventSub.Constants;

namespace TwitchySharp.Api.Helix.EventSub.SubscriptionTypes;

/// <summary>
/// A Hype Train ends on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) that you want to get Hype Train end notifications for.
/// This user must have also created a user access token including <see cref="Scope.ChannelReadHypeTrain"/> for your application.
/// </param>
public sealed record HypeTrainEndV2(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.HYPE_TRAIN_END;
    public string Version => EventSubSubscriptionTypeVersions.V2;

    private readonly EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
