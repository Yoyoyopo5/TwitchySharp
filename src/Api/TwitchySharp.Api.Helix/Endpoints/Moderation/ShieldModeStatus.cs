using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Moderation;

/// <summary>
/// Contains information about a channel's Shield Mode status.
/// </summary>
public record ShieldModeStatus
{
    /// <summary>
    /// Determines whether Shield Mode is active. 
    /// Is <see langword="true"/> if Shield Mode is active; otherwise, <see langword="false"/>.
    /// </summary>
    public required bool IsActive { get; init; }
    /// <summary>
    /// The user id of the moderator that last activated Shield Mode.
    /// </summary>
    public required UserId ModeratorId { get; init; }
    /// <summary>
    /// The login (username) of the moderator that last activated Shield Mode.
    /// </summary>
    public required UserLogin ModeratorLogin { get; init; }
    /// <summary>
    /// The display name of the moderator that last activated Shield Mode.
    /// </summary>
    public required UserName ModeratorName { get; init; }
    /// <summary>
    /// The date and time when Shield Mode was last activated.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public DateTimeOffset? LastActivatedAt { get; init; }
}
