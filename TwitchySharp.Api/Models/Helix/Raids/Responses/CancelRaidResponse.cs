using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Raids.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record CancelRaidResponse { }
