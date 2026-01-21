using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// A Twitch user's login (username).
/// </summary>
/// <remarks>
/// This is what users use to log in to Twitch with, and is always lower case.
/// It is also what appears in the url of their Twitch channel page.
/// Contrast this with <see cref="UserName"/>.
/// </remarks>
/// <param name="Value">
/// The string value of the user login. 
/// This will be made lower case.
/// </param>
[JsonConverter(typeof(WrapperJsonConverter<UserLogin, string>))]
public readonly record struct UserLogin(string Value) : IWrapValue<string>
{
    public string Value { get; } = Value.ToLower();
    public static implicit operator string(UserLogin login)
        => login.Value;
    public override string ToString()
        => Value;
}
