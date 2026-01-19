using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SendExtensionChatMessageResponse { }
