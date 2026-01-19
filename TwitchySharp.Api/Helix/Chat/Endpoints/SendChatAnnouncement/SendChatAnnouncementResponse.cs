using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SendChatAnnouncementResponse { }
