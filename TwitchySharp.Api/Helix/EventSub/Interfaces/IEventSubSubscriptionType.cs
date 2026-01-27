using System.Collections.Generic;
using TwitchySharp.Shared.EventSub.Enums;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// An EventSub subscription type.
/// </summary>
public interface IEventSubSubscriptionType
{
    /// <summary>
    /// The type of the subscription, combining name and version.
    /// </summary>
    /// <remarks>
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </remarks>
    EventSubSubscriptionType Type { get; }
    /// <summary>
    /// A dictionary that contains the parameter values that are specific to the specified subscription type.
    /// For the object's required and optional fields, see the subscription type's documentation.
    /// </summary>
    IReadOnlyDictionary<string, object> Condition { get; } // All conditions currently use string values, however it is possible that one may eventually appear with a non-string value, so let's use object here.
}
