namespace TwitchySharp.Api.Helix.Conduits;

/// <summary>
/// Contains data about an error that occurred when updating a conduit shard.
/// </summary>
public record ConduitUpdateError
{
    /// <summary>
    /// The id of the shard that had the error.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The error that occurred while updating the shard.
    /// </summary>
    public required string Message { get; init; }
    /// <summary>
    /// Error code used to represent a specific error condition while attempting to update a shard.
    /// </summary>
    public required string Code { get; init; }
}
