using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.Extensions.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record SetExtensionConfigurationSegmentResponse { }
