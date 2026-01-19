using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record UnbanUserResponse { }
