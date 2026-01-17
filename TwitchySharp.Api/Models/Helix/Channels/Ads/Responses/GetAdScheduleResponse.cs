using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Models.Helix.Channels.Ads.Models;
using TwitchySharp.Helpers.JsonConverters;

namespace TwitchySharp.Api.Models.Helix.Channels.Ads.Responses;
/// <summary>
/// Contains information about a channel's ad schedule.
/// </summary>
public record GetAdScheduleResponse
{
    /// <summary>
    /// A list that contains information related to the channel’s ad schedule.
    /// There should only be one entry?
    /// </summary>
    public required AdSchedule[] Data { get; init; }
}
