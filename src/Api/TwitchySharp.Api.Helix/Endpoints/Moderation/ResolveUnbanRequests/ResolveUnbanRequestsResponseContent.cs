namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of resolved unban requests.
/// </summary>
public record ResolveUnbanRequestsResponseContent
{
    /// <summary>
    /// A list containing the single resolved unban request.
    /// </summary>
    public required UnbanRequest[] Data { get; init; }
}
