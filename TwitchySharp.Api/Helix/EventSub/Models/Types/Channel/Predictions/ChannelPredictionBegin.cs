using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.Predictions;
/// <summary>
/// A Prediction started on a specified channel.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/>.
/// </remarks>
/// <param name="BroadcasterUserId">
/// The user id of the broadcaster for which prediction begin events will be received.
/// This user must have created a user access token including <see cref="Scope.ChannelReadPredictions"/> or <see cref="Scope.ChannelManagePredictions"/> for this application.
/// </param>
public sealed record ChannelPredictionBegin(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHANNEL_PREDICTION_BEGIN;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
