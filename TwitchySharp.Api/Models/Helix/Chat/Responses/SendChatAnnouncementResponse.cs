using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Chat.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SendChatAnnouncementResponse { }
