using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
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
    [JsonInclude, JsonRequired]
    public ExtensionTransactionData[] Data { get; private set; } = [];
    /// <summary>
    /// The cursor used to get the next page of results. Use the <see cref="Pagination.Cursor"/> property to set the request’s after parameter.
    /// </summary>
    [JsonInclude, JsonRequired]
    public Pagination Pagination { get; private set; } = new();
}

/// <summary>
/// Contains data about a specific extension transaction.
/// </summary>
public record ExtensionTransactionData
{
    /// <summary>
    /// An ID that identifies the transaction.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Id { get; private set; } = string.Empty;
    /// <summary>
    /// The date and time of the transaction.
    /// </summary>
    [JsonInclude, JsonRequired]
    public DateTimeOffset Timestamp { get; private set; }
    /// <summary>
    /// The user ID of the broadcaster that owns the channel where the transaction occurred.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterId { get; private set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s login name (username).
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterLogin { get; private set; } = string.Empty;
    /// <summary>
    /// The broadcaster’s display name.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string BroadcasterName { get; private set; } = string.Empty;
    /// <summary>
    /// The user ID of the user that purchased the digital product.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserId { get; private set; } = string.Empty;
    /// <summary>
    /// The user’s login name (username).
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserLogin { get; private set; } = string.Empty;
    /// <summary>
    /// The user’s display name.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string UserName { get; private set; } = string.Empty;
    /// <summary>
    /// The type of transaction. This should always be BITS_IN_EXTENSION.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string ProductType { get; private set; } = string.Empty;
    /// <summary>
    /// Contains details about the digital product.
    /// </summary>
    [JsonInclude, JsonRequired]
    public ExtensionProductData ProductData { get; private set; } = new();
    /// <summary>
    /// A boolean value that determines whether the product is in development.
    /// Is <see langword="true"/> if the digital product is in development and cannot be exchanged.
    /// </summary>
    [JsonInclude, JsonRequired]
    public bool InDevelopment { get; private set; }
    /// <summary>
    /// The name of the digital product.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string DisplayName { get; private set; } = string.Empty;
    /// <summary>
    /// This field is always empty since you may purchase only unexpired products.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Expiration { get; private set; } = string.Empty;
    /// <summary>
    /// A boolean value that determines whether the data was broadcast to all instances of the extension. 
    /// Is true if the data was broadcast to all instances.
    /// </summary>
    [JsonInclude, JsonRequired]
    public bool Broadcast { get; private set; }
}

/// <summary>
/// Contains details about a digital product for an extension (bits used in extension).
/// </summary>
public record ExtensionProductData
{
    /// <summary>
    /// An ID that identifies the digital product.
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Sku { get; private set; } = string.Empty;
    /// <summary>
    /// Set to: "twitch.ext.{the extension's ID}".
    /// </summary>
    [JsonInclude, JsonRequired]
    public string Domain { get; private set; } = string.Empty;
    /// <summary>
    /// Contains details about the digital product’s cost.
    /// </summary>
    [JsonInclude, JsonRequired]
    public ExtensionProductCost Cost { get; private set; } = new();
}

/// <summary>
/// Contains details about an extension digital product’s cost.
/// </summary>
public record ExtensionProductCost
{
    /// <summary>
    /// The amount exchanged for the digital product.
    /// Essentially, this is the amount of bits used.
    /// </summary>
    [JsonInclude, JsonRequired]
    public int Amount { get; private set; }
    /// <summary>
    /// The type of currency exchanged.
    /// As of now, this can only be bits.
    /// </summary>
    [JsonInclude, JsonRequired, JsonConverter(typeof(JsonStringEnumConverter))]
    public ExtensionProductCostType Type { get; private set; }
}

/// <summary>
/// Cost types for Twitch extension transactions.
/// </summary>
public enum ExtensionProductCostType
{
    Bits
}
