using System.Net.Http;
using TwitchySharp.Api.Models.Helix.CCLs.Enums;
using TwitchySharp.Api.Models.Helix.CCLs.Responses;
using TwitchySharp.Helpers;

namespace TwitchySharp.Api.Models.Helix.CCLs.Requests;

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
    /// <param name="locale">
    /// Locale to get content classification labels in.
    /// Defaults to <see cref="ContentClassificationLocale.EnglishUnitedStates"/>.
    /// </param>
    public GetContentClassificationLabelsRequest(
        string clientId,
        string accessToken,
        ContentClassificationLocale? locale = null
        )
        : base(
            "/content_classification_labels",
            clientId,
            accessToken,
            new HttpQueryParameters()
                .Add("locale", locale?.Value)
        )
    {
        Method = HttpMethod.Get;
    }
}
