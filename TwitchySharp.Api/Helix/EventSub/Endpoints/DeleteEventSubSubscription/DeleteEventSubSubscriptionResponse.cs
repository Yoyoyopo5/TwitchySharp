using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteEventSubSubscriptionResponse { }
