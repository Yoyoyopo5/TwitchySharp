using System.Text.Json.Serialization;
using TwitchySharp.Serialization;

namespace TwitchySharp.Api.Helix.Extensions;

/// <summary>
/// Contains data about a specific extension transaction.
/// </summary>
public record ExtensionTransactionData
{
    /// <summary>
    /// An ID that identifies the transaction.
    /// </summary>
    public required ExtensionTransactionId Id { get; init; }
    /// <summary>
    /// The date and time of the transaction.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }
    /// <summary>
    /// The user ID of the broadcaster that owns the channel where the transaction occurred.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    public required UserLogin BroadcasterLogin { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required UserName BroadcasterName { get; init; }
    /// <summary>
    /// The user ID of the user that purchased the digital product.
    /// </summary>
    public required UserId UserId { get; init; }
    /// <summary>
    /// The user’s login name (username).
    /// </summary>
    public required UserLogin UserLogin { get; init; }
    /// <summary>
    /// The user’s display name.
    /// </summary>
    public required UserName UserName { get; init; }
    /// <summary>
    /// The type of transaction. This should always be <see cref="ExtensionProductType.BitsInExtension"/>.
    /// </summary>
    public required ExtensionProductType ProductType { get; init; }
    /// <summary>
    /// Contains details about the digital product.
    /// </summary>
    public required ExtensionProductData ProductData { get; init; }
    /// <summary>
    /// A boolean value that determines whether the product is in development.
    /// Is <see langword="true"/> if the digital product is in development and cannot be exchanged.
    /// </summary>
    public required bool InDevelopment { get; init; }
    /// <summary>
    /// The name of the digital product.
    /// </summary>
    public required string DisplayName { get; init; }
    /// <summary>
    /// This field is always <see langword="null"/> since you may purchase only unexpired products.
    /// </summary>
    [JsonConverter(typeof(EmptyDateTimeOffsetConverter))]
    public required DateTimeOffset? Expiration { get; init; }
    /// <summary>
    /// Indicates whether the data was broadcast to all instances of the extension. 
    /// Is true if the data was broadcast to all instances.
    /// </summary>
    public required bool Broadcast { get; init; }
}
