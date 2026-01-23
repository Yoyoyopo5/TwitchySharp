using System;
using System.Net.Http;
using System.Text.Json.Serialization;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Helpers.JsonConverters;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Channels;
/// <summary>
/// Updates a channel’s properties.
/// </summary>
/// <remarks>
/// Requires a user access token with <see cref="Scope.ChannelManageBroadcast"/>.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#modify-channel-information">Modify Channel Information</see> for more information.
/// </remarks>
public record ModifyChannelInformationRequest
    : TwitchHelixRequest<ModifyChannelInformationResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">A user access token with <see cref="Scope.ChannelManageBroadcast"/>.</param>
    /// <param name="parameters">The request parameters.</param>
    /// <param name="channelInformation">
    /// The channel information to be set on the broadcaster's channel.
    /// </param>
    public ModifyChannelInformationRequest(
        ClientId clientId,
        UserAccessToken accessToken,
        ModifyChannelInformationRequestParameters parameters,
        ModifyChannelInformationRequestData channelInformation
        )
        : base(
            "/channels",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("broadcaster_id", parameters.BroadcasterId)
            )
    {
        Method = HttpMethod.Patch;
        ContentObject = channelInformation;
    }
}

/// <summary>
/// Request parameters for a <see cref="ModifyChannelInformationRequest"/>.
/// </summary>
public record ModifyChannelInformationRequestParameters
{
    /// <summary>
    /// The user id of the broadcaster whose channel you want to update.
    /// </summary>
    /// <remarks>
    /// This must be the same used that created the access token used in the request.
    /// </remarks>
    public required UserId BroadcasterId { get; set; }
}

/// <summary>
/// Contains data used to set channel information.
/// </summary>
public record ModifyChannelInformationRequestData
{
    /// <summary>
    /// The ID of the game that the user plays. 
    /// The game is not updated if the ID isn’t a game ID that Twitch recognizes. 
    /// To unset this field, use <c>"0"</c> or <see cref="string.Empty"/>.
    /// </summary>
    public GameId? GameId { get; set; }
    /// <summary>
    /// The user’s preferred language. 
    /// Set the value to an ISO 639-1 two-letter language code (for example, en for English). 
    /// Set to "other" if the user’s preferred language is not a Twitch supported language. 
    /// The language isn’t updated if the language code isn’t a Twitch supported language.
    /// </summary>
    public LanguageCode? BroadcasterLanguage { get; set; }
    /// <summary>
    /// The title of the user’s stream. 
    /// You may not set this field to an empty string.
    /// </summary>
    public string? Title { get; set; }
    /// <summary>
    /// The amount of time you want your broadcast buffered before streaming it live.
    /// The delay helps ensure fairness during competitive play. 
    /// Only users with Partner status may set this field. 
    /// The maximum delay is 900 seconds (15 minutes).
    /// </summary>
    [JsonConverter(typeof(SecondsTimeSpanJsonConverter))]
    public TimeSpan? Delay { get; set; }
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
    public string[]? Tags { get; set; }
    /// <summary>
    /// List of labels that should be set as the Channel’s CCLs.
    /// </summary>
    public ContentClassificationLabel[]? ContentClassificationLabels { get; set; }
    /// <summary>
    /// Boolean flag indicating whether the branded content label should be enabled or disabled for the channel.
    /// </summary>
    public bool? IsBrandedContent { get; set; }
}
