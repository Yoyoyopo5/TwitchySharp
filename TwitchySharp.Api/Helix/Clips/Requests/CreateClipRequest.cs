using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TwitchySharp.Helpers;
using TwitchySharp.Api.Authorization;

namespace TwitchySharp.Api.Helix.Clips;
/// <summary>
/// Creates a clip from the broadcaster’s stream.
/// This API captures up to 90 seconds of the broadcaster’s stream.
/// The 90 seconds spans the point in the stream from when you called the API. 
/// For example, if you call the API at the 4:00 minute mark, the API captures from approximately the 3:35 mark to approximately the 4:05 minute mark. 
/// Twitch tries its best to capture 90 seconds of the stream, but the actual length may be less. 
/// This may occur if you begin capturing the clip near the beginning or end of the stream.
/// <br/>
/// By default, Twitch publishes up to the last 30 seconds of the 90 seconds window and provides a default title for the clip. 
/// To specify the title and the portion of the 90 seconds window that’s used for the clip, use the URL in the response’s <see cref="CreateClipResponse.EditUrl"/> property. 
/// You can specify a clip that’s from 5 seconds to 60 seconds in length. The URL is valid for up to 24 hours or until the clip is published, whichever comes first.
/// <br/>
/// Creating a clip is an asynchronous process that can take a short amount of time to complete. 
/// To determine whether the clip was successfully created, call Get Clips using the <see cref="CreateClipResponse.Id"/> that this request returned. 
/// If Get Clips returns the clip, the clip was successfully created. If after 15 seconds Get Clips hasn’t returned the clip, assume it failed.
/// <br/>
/// See <see href="https://dev.twitch.tv/docs/api/reference/#create-clip">create clip</see> for more information.
/// <br/>
/// Requires a user access token that includes <see cref="Scope.ClipsEdit"/>.
/// </summary>
/// <param name="clientId">The client id of the application.</param>
/// <param name="accessToken">A user access token that includes <see cref="Scope.ClipsEdit"/>.</param>
/// <param name="broadcasterId">The user id of the broadcaster whose stream you want to create a clip from.</param>
/// <param name="hasDelay">
/// Determines whether the API captures the clip at the moment the viewer requests it or after a delay. 
/// If <see langword="false"/> (default), Twitch captures the clip at the moment the viewer requests it (this is the same clip experience as the Twitch UX). 
/// If <see langword="true"/>, Twitch adds a delay before capturing the clip (this basically shifts the capture window to the right slightly).
/// </param>
public class CreateClipRequest(string clientId, string accessToken, string broadcasterId, bool? hasDelay = null)
    : HelixApiRequest<CreateClipResponse>(
        "/clips" +
        new HttpQueryParameters()
            .Add("broadcaster_id", broadcasterId)
            .Add("has_delay", hasDelay?.ToString()),
        clientId,
        accessToken
        )
{
    public override HttpMethod Method => HttpMethod.Post;
}
