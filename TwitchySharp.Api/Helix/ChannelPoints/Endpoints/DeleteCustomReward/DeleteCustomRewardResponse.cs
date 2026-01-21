using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.ChannelPoints;
/// <summary>
/// Contains no data.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteCustomRewardResponse { }
