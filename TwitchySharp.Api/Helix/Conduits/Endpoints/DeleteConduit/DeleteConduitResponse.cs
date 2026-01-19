using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Conduits;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteConduitResponse { }
