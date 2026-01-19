using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Empty response. Contains no data.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record ModifyChannelInformationResponse { }
