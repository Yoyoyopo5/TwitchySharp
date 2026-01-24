using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Streams;
/// <summary>
/// An id representing a specific Twitch stream marker.
/// </summary>
/// <param name="Value">The string value of the id.</param>
[JsonConverter(typeof(WrapperJsonConverter<StreamMarkerId, string>))]
public readonly record struct StreamMarkerId(string Value) : IWrapValue<string>
{
    public static implicit operator string(StreamMarkerId id)
        => id.Value;
    public override string ToString()
        => Value;
}
