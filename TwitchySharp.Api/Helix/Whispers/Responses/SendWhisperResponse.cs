using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Whispers;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SendWhisperResponse { }