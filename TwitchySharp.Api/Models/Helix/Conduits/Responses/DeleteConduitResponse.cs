using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Conduits.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteConduitResponse { }
