using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Moderation;
/// <summary>
/// Contains a list of blocked terms on a specific channel.
/// </summary>
public record GetBlockedTermsResponse
{
    /// <summary>
    /// The list of blocked terms.
    /// The list is in descending order by <see cref="BlockedTerm.CreatedAt"/> (newest first).
    /// </summary>
    public required BlockedTerm[] Data { get; init; }
    /// <inheritdoc cref="Models.Pagination"/>
    public required Pagination Pagination { get; init; }
}

/// <summary>
/// Contains information about a specific blocked term in a channel's chat.
/// </summary>
public record BlockedTerm
{
    /// <summary>
    /// The user id of the broadcaster (channel) that the blocked term belongs to.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The user id of the moderator (or broadcaster) that created the blocked term.
    /// </summary>
    public required string ModeratorId { get; init; }
    /// <summary>
    /// The id of the blocked term.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The blocked word or phrase.
    /// </summary>
    public required string Text { get; init; }
    /// <summary>
    /// The date and time that the term was blocked.
    /// </summary>
    public required DateTimeOffset CreatedAt { get; init; }
    /// <summary>
    /// The date and time that the term was last updated.
    /// When the term is first created, this is the same as <see cref="CreatedAt"/>.
    /// This timestamp changes as AutoMod continues to deny the term.
    /// </summary>
    public required DateTimeOffset UpdatedAt { get; init; }
    /// <summary>
    /// The date and time that the blocked term is set to expire.
    /// This is <see langword="null"/> if the term was added manually or was permanently blocked by AutoMod.
    /// </summary>
    public DateTimeOffset? ExpiresAt { get; init; }
}
