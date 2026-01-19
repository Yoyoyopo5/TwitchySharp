using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.GuestStar;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteGuestStarInviteResponse { }
