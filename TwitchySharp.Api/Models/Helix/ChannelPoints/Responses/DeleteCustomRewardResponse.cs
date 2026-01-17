using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.ChannelPoints.Responses;
/// <summary>
/// Contains no data.
/// </summary>;
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteCustomRewardResponse { }
