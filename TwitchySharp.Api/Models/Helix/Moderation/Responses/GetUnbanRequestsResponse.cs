using TwitchySharp.Api.Models.Helix.Moderation.Models;
using TwitchySharp.Api.Models.Shared;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Contains a list of unban requests for a specific channel.
/// </summary>
public record GetUnbanRequestsResponse
{
    /// <summary>
    /// The list of unban requests.
    /// </summary>
    public required UnbanRequest[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}
