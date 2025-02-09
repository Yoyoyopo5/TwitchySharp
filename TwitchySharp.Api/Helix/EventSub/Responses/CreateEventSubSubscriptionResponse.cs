using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Contains a list of created EventSub subscriptions.
/// </summary>
public record CreateEventSubSubscriptionResponse
{
    /// <summary>
    /// A list containing the single subscription that was created.
    /// </summary>
    public required EventSubSubscription[] Data { get; init; }
    /// <summary>
    /// The total number of subscriptions the application has created.
    /// </summary>
    public required int Total { get; init; }
    /// <summary>
    /// The sum of all your <see href="https://dev.twitch.tv/docs/eventsub/manage-subscriptions/#subscription-limits">subscription costs</see>.
    /// </summary>
    public required int TotalCost { get; init; }
    /// <summary>
    /// The maximum total cost that you’re allowed to incur for all subscriptions you create.
    /// </summary>
    public required int MaxTotalCost { get; init; }
}
