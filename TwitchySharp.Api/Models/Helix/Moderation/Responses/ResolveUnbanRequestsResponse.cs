using TwitchySharp.Api.Models.Helix.Moderation.Models;

namespace TwitchySharp.Api.Models.Helix.Moderation.Responses;
/// <summary>
/// Contains a list of resolved unban requests.
/// </summary>
public record ResolveUnbanRequestsResponse
{
    /// <summary>
    /// A list containing the single resolved unban request.
    /// </summary>
    public required UnbanRequest[] Data { get; init; }
}
