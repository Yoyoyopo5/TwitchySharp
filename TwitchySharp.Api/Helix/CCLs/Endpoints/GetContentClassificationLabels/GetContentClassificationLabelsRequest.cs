using System.Net.Http;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.CCLs;

/// <summary>
/// Gets information about Twitch content classification labels.
/// </summary>
/// <remarks>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-content-classification-labels">Get Content Classification Labels</see> for more information.
/// </remarks>
public record GetContentClassificationLabelsRequest
    : TwitchHelixRequest<GetContentClassificationLabelsResponse>
{
    /// <param name="clientId">The client id of the application.</param>
    /// <param name="accessToken">An app or user access token.</param>
    /// <param name="parameters">The request parameters.</param>
    public GetContentClassificationLabelsRequest(
        ClientId clientId,
        AccessToken accessToken,
        GetContentClassificationLabelsRequestParameters? parameters = null
        )
        : base(
            "/content_classification_labels",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("locale", parameters?.Locale?.Value)
        )
    {
        Method = HttpMethod.Get;
    }
}

/// <summary>
/// Request parameters for a <see cref="GetContentClassificationLabelsRequest"/>.
/// </summary>
public record GetContentClassificationLabelsRequestParameters
{
    /// <summary>
    /// Locale to get content classification labels in.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="ContentClassificationLocale.EnglishUnitedStates"/> if left <see langword="null"/>.
    /// </remarks>
    public ContentClassificationLocale? Locale { get; set; }
}
