using System.Collections.Generic;
using System.Net.Http;
using TwitchySharp.Api.Authorization;
using TwitchySharp.Helpers;
using TwitchySharp.Shared.Models;

namespace TwitchySharp.Api.Helix.Bits;
/// <summary>
/// Gets a list of Cheermotes that users can use to cheer Bits in any Bits-enabled channel's chat room.
/// </summary>
/// <remarks>
/// Cheermotes are animated emotes that viewers can assign Bits to.
/// <br/>
/// Requires an app or user access token.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#get-cheermotes">Get Cheermotes</see> for more information.
/// </remarks>
public record GetCheermotesRequest
    : TwitchHelixRequest<GetCheermotesResponse>
{
    protected override string Path => "/bits/cheermotes";
    public override HttpMethod Method => HttpMethod.Get;
    protected override HttpQueryParameters QueryParameters
        => new HttpQueryParameters()
            .Add("broadcaster_id", BroadcasterId);

    /// <summary>
    /// The user id of the broadcaster whose custom Cheermotes you want to get.
    /// </summary>
    /// <remarks>
    /// Specify this if you want to include the broadcaster's Cheermotes in the response (not all broadcasters upload Cheermotes).
    /// If <see langword="null"/>, the response contains only global Cheermotes.
    /// If the broadcaster uploaded Cheermotes, the <see cref="Cheermote.Type"/> in the response is set to <see cref="CheermoteType.ChannelCustom"/>.
    /// </remarks>
    public UserId? BroadcasterId { get; init; }
}
