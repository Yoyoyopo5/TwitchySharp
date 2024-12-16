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
}
