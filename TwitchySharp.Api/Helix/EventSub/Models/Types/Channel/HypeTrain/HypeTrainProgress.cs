using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.HypeTrain;
/// <summary>
/// A Hype Train makes progress on the specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token that includes <see cref="Scope.ChannelReadHypeTrain"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster (channel) that you want to get Hype Train progress notifications for.
/// This user must have also created a user access token including <see cref="Scope.ChannelReadHypeTrain"/> for your application.
/// </param>
public sealed record HypeTrainProgress(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.HYPE_TRAIN_PROGRESS;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
