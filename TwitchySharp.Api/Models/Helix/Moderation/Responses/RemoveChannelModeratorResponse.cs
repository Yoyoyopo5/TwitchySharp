using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record RemoveChannelModeratorResponse { }
