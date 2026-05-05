namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Contains a list of chatters in a broadcaster's channel.
/// </summary>
public record GetChattersResponse
    : IPageableResponse
{
    /// <summary>
    /// The list of users that are connected to the broadcaster’s chat room.
    /// The list is empty if no users are connected to the chat room.
    /// </summary>
    public required Chatter[] Data { get; init; }
    /// <summary>
    /// Contains the information used to page through the list of results.
    /// The <see cref="Pagination.Cursor"/> is null if there are no more pages left to page through.
    /// </summary>
    public required Pagination Pagination { get; init; }
    /// <summary>
    /// The total number of users that are connected to the broadcaster’s chat room. 
    /// As you page through the list, the number of users may change as users join and leave the chat room.
    /// </summary>
    public required int Total { get; init; }
}