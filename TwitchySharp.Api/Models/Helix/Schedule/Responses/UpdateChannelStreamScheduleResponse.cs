using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Schedule.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record UpdateChannelStreamScheduleResponse { }
