using TwitchySharp.Helpers;

namespace TwitchySharp.Shared.Models;

/// <summary>
/// An id representing a specific Twitch organization.
/// </summary>
/// <remarks>
/// See <see href="https://dev.twitch.tv/docs/companies/">Organizations</see> for more information.
/// </remarks>
/// <param name="Value">The string value of the id</param>
public readonly partial record struct OrganizationId(string Value) : IWrapValue<string>;