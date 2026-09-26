namespace TwitchySharp.Api.Helix.Chat;
/// <summary>
/// Gets the broadcaster's list of custom emotes.
/// </summary>
/// <remarks>
/// Broadcasters create these custom emotes for users who subscribe to or follow the channel or cheer Bits in the channel's chat window.
/// For information about the custom emotes, see <see href="https://help.twitch.tv/s/article/subscriber-emote-guide">Subscriber Emotes</see>, <see href="https://help.twitch.tv/s/article/custom-bit-badges-guide?language=bg#slots">Bits Tier Emotes</see>, and <see href="https://blog.twitch.tv/en/2021/06/04/kicking-off-10-years-with-our-biggest-emote-update-ever/">Follower Emotes</see>.
/// <b>NOTE:</b> With the exception of custom follower emotes, users may use custom emotes in any Twitch chat.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-channel-emotes">Get Channel Emotes</see> for more information.
/// </remarks>
public record GetChannelEmotesRequest
    : TwitchHelixRequest<GetChannelEmotesResponseContent>,
    IAuthenticatedTwitchRequest<ITwitchRequestAuthenticationContext<TwitchIdentity>>
{
    protected override string Path => "/chat/emotes";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);
    public ITwitchRequestAuthenticationContext<TwitchIdentity> AuthenticationContext { get; init; }
        = TwitchRequestAuthenticationContext.Default;

    /// <summary>
    /// The user id of the broadcaster (channel) whose emotes you want to get.
    /// </summary>
    public required UserId BroadcasterId { get; init; }
}
