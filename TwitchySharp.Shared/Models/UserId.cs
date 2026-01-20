using System.Text.Json;
using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;
/// <summary>
/// An id representing a specific Twitch user.
/// </summary>
/// <remarks>
/// Note that while users can change their logins and display names, the id will never change.
/// </remarks>
/// <param name="Value">The string value of the user id.</param>
[JsonConverter(typeof(WrapperJsonConverter<UserId, string>))]
public readonly record struct UserId(string Value) : IWrapValue<string>
{
    public static implicit operator string(UserId id)
        => id.Value;
    public override string ToString()
        => Value;
}
