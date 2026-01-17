using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Channels.Responses;
/// <summary>
/// Empty response. Contains no data.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record ModifyChannelInformationResponse { }
