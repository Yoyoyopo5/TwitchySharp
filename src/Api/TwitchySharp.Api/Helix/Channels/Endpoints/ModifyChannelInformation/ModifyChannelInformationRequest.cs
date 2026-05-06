using System;
using System.Collections.Immutable;
using System.IO;
using System.Net.Http;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Updates a channel's properties.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelManageBroadcast"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#modify-channel-information">Modify Channel Information</see> for more information.
/// </remarks>
public record ModifyChannelInformationRequest
    : TwitchHelixRequest<ModifyChannelInformationResponse>
{
    protected override string Path => "/channels";
    public override HttpMethod Method => HttpMethod.Patch;
    protected override TwitchRequestAuthorizationContext DefaultAuthorizationContext => new()
    {
        Identity = new TwitchIdentity.User(BroadcasterId),
        ValidScopes = ImmutableHashSet.Create(Scope.ChannelManageBroadcast)
    };
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public override object? ContentObject => ChannelInformation;

    /// <summary>
    /// The user id of the broadcaster whose channel you want to update.
    /// </summary>
    /// <remarks>
    /// This must be the same user that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; init; }

    /// <summary>
    /// The channel information to be set on the broadcaster's channel.
    /// </summary>
    public required ModifyChannelInformationRequestData ChannelInformation { get; init; }

    protected override ValueTask<ModifyChannelInformationResponse> ConvertResponseContent(Stream contentStream, CancellationToken ct = default)
         => ValueTask.FromResult(new ModifyChannelInformationResponse());
}

/// <summary>
/// Contains data used to set channel information.
/// </summary>
public record ModifyChannelInformationRequestData
{
    /// <summary>
    /// The ID of the game that the user plays. 
    /// The game is not updated if the ID isnÅft a game ID that Twitch recognizes. 
    /// To unset this field, use <c>"0"</c> or <see cref="string.Empty"/>.
    /// </summary>
    public GameId? GameId { get; init; }
    /// <summary>
    /// The userÅfs preferred language. 
    /// Set the value to an ISO 639-1 two-letter language code (for example, en for English). 
    /// Set to "other" if the userÅfs preferred language is not a Twitch supported language. 
    /// The language isnÅft updated if the language code isnÅft a Twitch supported language.
    /// </summary>
    public LanguageCode? BroadcasterLanguage { get; init; }
    /// <summary>
    /// The title of the userÅfs stream. 
    /// You may not set this field to an empty string.
    /// </summary>
    public string? Title { get; init; }
    /// <summary>
    /// The amount of time you want your broadcast buffered before streaming it live.
    /// The delay helps ensure fairness during competitive play. 
    /// Only users with Partner status may set this field. 
    /// The maximum delay is 900 seconds (15 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? Delay { get; init; }
    /// <summary>
    /// A list of channel-defined tags to apply to the channel. 
    /// To remove all tags from the channel, set to an empty array.
    /// Tags help identify the content that the channel streams. <see href="https://help.twitch.tv/s/article/guide-to-tags">Learn More</see>.
    /// <br/>
    /// A channel may specify a maximum of 10 tags.
    /// Each tag is limited to a maximum of 25 characters and may not be an empty string or contain spaces or special characters.
    /// Tags are case insensitive.
    /// For readability, consider using camelCasing or PascalCasing.
    /// </summary>
    public string[]? Tags { get; init; }
    /// <summary>
    /// List of labels that should be set as the ChannelÅfs CCLs.
    /// </summary>
    public ContentClassificationLabel[]? ContentClassificationLabels { get; init; }
    /// <summary>
    /// Boolean flag indicating whether the branded content label should be enabled or disabled for the channel.
    /// </summary>
    public bool? IsBrandedContent { get; init; }
}
