using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Empty response.
/// </summary>
/// <remarks>
/// Dev Note: Docs are ambiguous on this response. Please update during integration testing.
/// </remarks>
[ApiConverter(typeof(EmptyResponseConverter))]
public record AssignGuestStarSlotResponse { }
