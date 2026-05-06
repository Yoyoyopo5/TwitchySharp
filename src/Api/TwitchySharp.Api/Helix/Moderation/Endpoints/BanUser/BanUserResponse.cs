namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains information about a newly banned or timed out user.
/// </summary>
public record BanUserResponse
{
    /// <summary>
    /// A list that contains the information about single user that was banned or timed out.
    /// </summary>
    public required UserBan[] Data { get; init; }
}