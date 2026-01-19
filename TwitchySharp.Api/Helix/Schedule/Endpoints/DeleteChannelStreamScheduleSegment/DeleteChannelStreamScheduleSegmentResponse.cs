using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Schedule;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteChannelStreamScheduleSegmentResponse { }
