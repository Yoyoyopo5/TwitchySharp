using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.Schedule;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record UpdateChannelStreamScheduleSegment
{
    /// <summary>
    /// The broadcaster's updated streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; }
}
