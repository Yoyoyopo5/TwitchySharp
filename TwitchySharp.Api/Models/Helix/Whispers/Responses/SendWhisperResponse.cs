using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Whispers.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SendWhisperResponse { }