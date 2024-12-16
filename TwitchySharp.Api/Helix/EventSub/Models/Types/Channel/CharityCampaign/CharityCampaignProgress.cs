using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub.Models.Types.Channel.CharityCampaign;
/// <summary>
/// Sends an event notification when progress is made towards the campaign’s goal or when the broadcaster changes the fundraising goal.
/// </summary>
/// <param name="BroadcasterUserId">The ID of the broadcaster that you want to receive notifications about when their campaign makes progress or is updated.</param>
public sealed record CharityCampaignProgress(string BroadcasterUserId)
    : IEventSubSubscriptionType
{
    public string Type => EventSubSubscriptionTypeNames.CHARITY_CAMPAIGN_PROGRESS;
    public string Version => EventSubSubscriptionTypeVersions.V1;

    private EventSubSubscriptionCondition _condition =
        new EventSubSubscriptionCondition()
            .Set("broadcaster_user_id", BroadcasterUserId);
    public IReadOnlyDictionary<string, object> Condition => _condition;
}
