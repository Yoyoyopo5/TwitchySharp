using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.GuestStar.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SendGuestStarInviteResponse { }
