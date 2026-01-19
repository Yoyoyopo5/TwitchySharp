using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Raids;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record CancelRaidResponse { }
