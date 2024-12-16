using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Contains a list of existing EventSub subscriptions.
/// </summary>
public record GetEventSubSubscriptionsResponse
{
    /// <summary>
    /// The list of subscriptions ordered by creation time (ascending).
    /// </summary>
    public required EventSubSubscription[] Data { get; init; }
    /// <summary>
    /// Used to get the next page of subscriptions. 
    /// The <see cref="Pagination.Cursor"/> is <see langword="null"/> if there are no more pages to get.
    /// </summary>
    public required Pagination Pagination { get; init; }
}
