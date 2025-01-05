using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Schedule;
/// <inheritdoc cref="ChannelStreamSchedule"/>
public record GetChannelStreamScheduleResponse
{
    /// <summary>
    /// The broadcaster’s streaming schedule.
    /// </summary>
    public required ChannelStreamSchedule Data { get; init; } // Interestingly, not an array this time.
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
