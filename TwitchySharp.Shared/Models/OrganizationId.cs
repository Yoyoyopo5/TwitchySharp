using System.Text.Json.Serialization;
using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch organization.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/companies/">Organizations</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the id</param>
[JsonConverter(typeof(WrapperJsonConverter<OrganizationId, string>))]
public readonly record struct OrganizationId(string Value) : IWrapValue<string>
{
    public static implicit operator string(OrganizationId id)
        => id.Value;
    public override string ToString()
        => Value;
}