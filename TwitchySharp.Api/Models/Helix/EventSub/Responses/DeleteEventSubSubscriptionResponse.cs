using TwitchySharp.Api.ResponseConverters;

namespace TwitchySharp.Api.Models.Helix.EventSub.Responses;
/// <summary>
/// Empty response.
/// </summary>
[ApiConverter(typeof(EmptyResponseConverter))]
public record DeleteEventSubSubscriptionResponse { }
