namespace TwitchySharp.Api.Helix.CCLs;

/// <summary>
/// Gets information about Twitch content classification labels.
/// </summary>
/// <remarks>
/// <para>
/// Requires an app or user access token.
/// </para>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-content-classification-labels">Get Content Classification Labels</see> for more information.
/// </remarks>
public record GetContentClassificationLabelsRequest
    : TwitchHelixRequest<GetContentClassificationLabelsResponseContent>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/content_classification_labels";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("locale", Locale?.Value);
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext
    {
        get => field ?? TwitchRequestAuthenticationContext.Default;
        init;
    }

    /// <summary>
    /// Locale to get content classification labels in.
    /// </summary>
    /// <remarks>
    /// Defaults to <see cref="ContentClassificationLocale.EnglishUnitedStates"/> if left <see langword="null"/>.
    /// </remarks>
    public ContentClassificationLocale? Locale { get; init; }
}
