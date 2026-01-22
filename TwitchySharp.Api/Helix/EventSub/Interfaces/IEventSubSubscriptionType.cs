using System.Collections.Generic;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.EventSub;
/// <summary>
/// An EventSub subscription type.
/// </summary>
public interface IEventSubSubscriptionType
{
    /// <summary>
    /// The type name of the subscription that will be created.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </summary>
    EventSubSubscriptionTypeName Name { get; } // Twitch API uses "type", but we will use "Name" as "Type" encompasses the name + version of the subscription.
    /// <summary>
    /// The version number that identifies the definition of the subscription type that the response will use.
    /// </summary>
    EventSubSubscriptionTypeVersion Version { get; }
    /// <summary>
    /// A dictionary that contains the parameter values that are specific to the specified subscription type. 
    /// For the object’s required and optional fields, see the subscription type’s documentation.
    /// </summary>
    IReadOnlyDictionary<string, object> Condition { get; } // All conditions currently use string values, however it is possible that one may eventually appear with a non-string value, so let's use object here.
}
