namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets emotes for one or more specified emote sets.
/// </summary>
/// <remarks>
/// An emote set groups emotes that have a similar context.
/// For example, Twitch places all the subscriber emotes that a broadcaster uploads for their channel in the same emote set.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-emote-sets">Get Emote Sets</see> for more information.
/// </remarks>
public record GetEmoteSetsRequest
    : TwitchHelixRequest<GetEmoteSetsResponseContent>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/chat/emotes/set";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("emote_set_id", EmoteSetIds.Select(x => x.ToString()));
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;

    /// <summary>
    /// A list of ids for the emote sets to get.
    /// </summary>
    /// <remarks>
    /// You may specify a maximum of 25 IDs.
    /// The response contains only the IDs that were found and ignores duplicate IDs.
    /// </remarks>
    public required IEnumerable<EmoteSetId> EmoteSetIds { get; init; }
}
