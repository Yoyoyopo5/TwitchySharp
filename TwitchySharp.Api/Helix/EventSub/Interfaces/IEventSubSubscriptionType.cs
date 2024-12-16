using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchySharp.Api.Helix.EventSub;
public interface IEventSubSubscriptionType
{
    /// <summary>
    /// The type name of the subscription that will be created.
    /// See <see href="https://dev.twitch.tv/docs/eventsub/eventsub-subscription-types#subscription-types">Subscription Types</see>.
    /// </summary>
    string Type { get; }
    /// <summary>
    /// The version number that identifies the definition of the subscription type that the response will use.
    /// </summary>
    string Version { get; }
    /// <summary>
    /// A dictionary that contains the parameter values that are specific to the specified subscription type. 
    /// For the object’s required and optional fields, see the subscription type’s documentation.
    /// </summary>
    IReadOnlyDictionary<string, object> Condition { get; } // All conditions currently use string values, however it is possible that one may eventually appear with a non-string value, so let's use object here.
}
