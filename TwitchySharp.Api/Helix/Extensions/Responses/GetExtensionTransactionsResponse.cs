using System;
using System.Collections.Generic;
using System.Text;
using TwitchySharp.Api.Models;

namespace TwitchySharp.Api.Helix.Extensions;
/// <summary>
/// Contains a list of extension transactions.
/// </summary>
public record GetExtensionTransactionsResponse
{
    /// <summary>
    /// The list of transactions.
    /// </summary>
    public required ExtensionTransactionData[] Data { get; init; }
    /// <summary>
    /// The cursor used to get the next page of results. Use the <see cref="Pagination.Cursor"/> property to set the request’s after parameter.
    /// </summary>
    public Pagination? Pagination { get; init; }
}

/// <summary>
/// Contains data about a specific extension transaction.
/// </summary>
public record ExtensionTransactionData
{
    /// <summary>
    /// An ID that identifies the transaction.
    /// </summary>
    public required string Id { get; init; }
    /// <summary>
    /// The date and time of the transaction.
    /// </summary>
    public required DateTimeOffset Timestamp { get; init; }
    /// <summary>
    /// The user ID of the broadcaster that owns the channel where the transaction occurred.
    /// </summary>
    public required string BroadcasterId { get; init; }
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    public required string BroadcasterLogin { get; init; }
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    public required string BroadcasterName { get; init; }
    /// <summary>
    /// The user ID of the user that purchased the digital product.
    /// </summary>
    public required string UserId { get; init; }
    /// <summary>
    /// The user’s login name (username).
    /// </summary>
    public required string UserLogin { get; init; }
    /// <summary>
    /// The user’s display name.
    /// </summary>
    public required string UserName { get; init; }
    /// <summary>
    /// The type of transaction. This should always be BITS_IN_EXTENSION.
    /// </summary>
    public required string ProductType { get; init; }
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
    /// This field is always empty since you may purchase only unexpired products.
    /// </summary>
    public required string Expiration { get; init; }
    /// <summary>
    /// A boolean value that determines whether the data was broadcast to all instances of the extension. 
    /// Is true if the data was broadcast to all instances.
    /// </summary>
    public required bool Broadcast { get; init; }
}

/// <summary>
/// Contains details about a digital product for an extension (bits used in extension).
/// </summary>
public record ExtensionProductData
{
    /// <summary>
    /// An ID that identifies the digital product.
    /// </summary>
    public required string Sku { get; init; }
    /// <summary>
    /// Set to: "twitch.ext.{the extension's ID}".
    /// </summary>
    public required string Domain { get; init; }
    /// <summary>
    /// Contains details about the digital product’s cost.
    /// </summary>
    public required ExtensionProductCost Cost { get; init; }
}
