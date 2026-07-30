namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of user's and their selected chat colors.
/// </summary>
public record GetUserChatColorResponse
{
    /// <summary>
    /// The list of users.
    /// </summary>
    public required UserChatColor[] Data { get; init; }
}
