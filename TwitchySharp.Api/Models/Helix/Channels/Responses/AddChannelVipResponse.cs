using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Channels.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record AddChannelVipResponse { }
