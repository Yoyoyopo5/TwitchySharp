using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record AddChannelVipResponse { }
